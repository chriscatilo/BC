using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using BC.EQCS.Repositories.Utils;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories.Security
{
    public class ActiveDirectoryUserRepository : IActiveDirectoryUserRepository
    {
        public const int MaxResults = 50; //TODO: add to options 
        internal class PropertyNames
        {
            internal const string EmailAddress = "mail";
            internal const string FirstName = "givenName";
            internal const string Surname = "sn";
            internal const string TelephoneNumber = "telephoneNumber";
            internal const string ObjectGuid = "objectGUID";
            internal const string Country = "country";
            internal const string Department = "department";
            internal const string SamAccountName = "SAMAccountName";

        }

        private const string DefaultCountry = "United Kingdom";

        public async Task<ActiveDirectoryUser> GetUserByObjectGuid(Guid objectGuid)
        {
            if (objectGuid == Guid.Empty)
            {
                throw new ArgumentNullException("objectGuid");
            }

            var octetGuid = ConvertGuidToOctectString(objectGuid);
            return await Task.Run(() =>
            {
                using (var entry = GetRootEntry())
                {
                    using (var dirSearcher = new DirectorySearcher(entry))
                    {
                        dirSearcher.PageSize = 1;
                        dirSearcher.Filter =
                            string.Format("(&(objectClass=user)(objectcategory=person)(objectGUID={0}))",
                                octetGuid);
                        AddPropertiesToLoad(dirSearcher);
                        var searchResult = dirSearcher.FindOne();

                        if (searchResult == null)
                        {
                            throw new UserNotFoundException();
                        }

                        var applicationUser = GenerateApplicationUser(searchResult);

                        return applicationUser;
                    }
                }
            });
        }

        public async Task<IEnumerable<ActiveDirectoryUser>> GetUsersBySearchFilter(SearchFilter filter)
        {
            if (filter == null ||
                (string.IsNullOrWhiteSpace(filter.FirstName) && string.IsNullOrWhiteSpace(filter.Surname) &&
                 filter.ObjectGuid == null))
            {
                throw new ArgumentNullException("filter");
            }
            return await Task.Run(() =>
            {
                using (var entry = GetRootEntry())
                {
                    using (var dirSearcher = new DirectorySearcher(entry))
                    {
                        dirSearcher.PageSize = MaxResults + 1;
                        const string firstNameFilterTemplate = "(givenName={0}*)";
                        const string surnameFilterTemplate = "(sn={0}*)";
                        const string objectGuidFilterTemplate = "(objectGuid={0}*)";
                        const string filterTemplate = "(&(objectClass=user)(objectcategory=person){0}{1}{2})";

                        var firstNameFilter = string.IsNullOrWhiteSpace(filter.FirstName)
                            ? string.Empty
                            : string.Format(firstNameFilterTemplate, filter.FirstName);

                        var surnameFilter = string.IsNullOrWhiteSpace(filter.Surname)
                            ? string.Empty
                            : string.Format(surnameFilterTemplate, filter.Surname);

                        var objectGuidFilter = filter.ObjectGuid != null && filter.ObjectGuid != Guid.Empty
                            ? string.Empty
                            : string.Format(objectGuidFilterTemplate, filter.ObjectGuid);

                        dirSearcher.Filter = string.Format(filterTemplate, firstNameFilter, surnameFilter,
                            objectGuidFilter);

                        AddPropertiesToLoad(dirSearcher);
                        var searchResults = dirSearcher.FindAll();
                        if (searchResults.Count > MaxResults)
                        {
                            throw new ActiveDirectoryResultsLimitExceededException(MaxResults);
                        }

                        var applicationUsers = (searchResults.Cast<SearchResult>().Select(GenerateApplicationUser));

                        return filter.ObjectGuid == null || filter.ObjectGuid == Guid.Empty
                            ? applicationUsers.ToList()
                            : applicationUsers.Where(user => user.ObjectGuid == filter.ObjectGuid).ToList();
                    }
                }
            });

        }
       
        private static ActiveDirectoryUser GenerateApplicationUser(SearchResult searchResult)
        {
            var applicationUser = new ActiveDirectoryUser
            {
                Login = searchResult.GetSearchResultPropertyAsString(PropertyNames.SamAccountName),
                Email = searchResult.GetSearchResultPropertyAsString(PropertyNames.EmailAddress),
                FirstName = searchResult.GetSearchResultPropertyAsString(PropertyNames.FirstName),
                Surname = searchResult.GetSearchResultPropertyAsString(PropertyNames.Surname),
                Telephone = searchResult.GetSearchResultPropertyAsString(PropertyNames.TelephoneNumber),
                Department = searchResult.GetSearchResultPropertyAsString(PropertyNames.Department),
                Country = searchResult.GetSearchResultPropertyAsString(PropertyNames.Country),
                ObjectGuid = searchResult.GetSearchResultPropertyAsGuid(PropertyNames.ObjectGuid)
            };

            // TODO: applicationUser.SetDisplayName();

            if (string.IsNullOrWhiteSpace(applicationUser.Country))
            {
                applicationUser.Country = DefaultCountry;
            }
            return applicationUser;
        }

        private static void AddPropertiesToLoad(DirectorySearcher dirSearcher)
        {
            dirSearcher.PropertiesToLoad.Add(PropertyNames.Surname);
            dirSearcher.PropertiesToLoad.Add(PropertyNames.FirstName);
            dirSearcher.PropertiesToLoad.Add(PropertyNames.EmailAddress);
            dirSearcher.PropertiesToLoad.Add(PropertyNames.TelephoneNumber);
            dirSearcher.PropertiesToLoad.Add(PropertyNames.Department);
            dirSearcher.PropertiesToLoad.Add(PropertyNames.Country);
            dirSearcher.PropertiesToLoad.Add(PropertyNames.ObjectGuid);
            dirSearcher.PropertiesToLoad.Add(PropertyNames.SamAccountName);
        }
        
        private DirectoryEntry GetRootEntry()
        {
            var username = ConfigurationManager.AppSettings["ADServiceAccount"];
            var password = ConfigurationManager.AppSettings["ADServicePassword"];
            var entry = new DirectoryEntry("LDAP://RootDSE", username, password);
            var str = entry.Properties["defaultNamingContext"][0];
            entry.Dispose();
            return new DirectoryEntry("LDAP://" + str, username, password);
        }

        private static string ConvertGuidToOctectString(Guid objectGuid)
        {
            byte[] byteGuid = objectGuid.ToByteArray();

            string queryGuid = "";

            foreach (byte b in byteGuid)
            {
                queryGuid += @"\" + b.ToString("x2");
            }

            return queryGuid;
        }
    }
}