using System;
using System.Collections.Generic;
using System.Net.Http;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Repositories.Security;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Integration
{
    public partial class Client
    {
        public IEnumerable<ActiveDirectoryUser> SearchUsersInActiveDirectory(SearchFilter search)
        {
            using (var httpClient = CreateHttpClient())
            {
                var response =
                    httpClient.GetAsync(string.Format(Constants.Urls.UserAdminSearchFirstNameSurname, search.FirstName,
                        search.Surname)).Result;

                return response.Content.ReadAsAsync<IEnumerable<ActiveDirectoryUser>>().Result;
            }
        }

        public HttpResponseMessage CreateUserFromActiveDirectory(Guid objectGuid)
        {
            using (var httpClient = CreateHttpClient())
            {
                return httpClient.PutAsync(string.Format(Constants.Urls.UserAdminUserUri, objectGuid), null).Result;
            }
        }

        public SecurityUserModel GetUser(Guid objectGuid)
        {
            using (var httpClient = CreateHttpClient())
            {
                return
                    httpClient.GetAsync(string.Format(Constants.Urls.UserAdminUserUri, objectGuid))
                        .Result.Content.ReadAsAsync<SecurityUserModel>()
                        .Result;
            }
        }

        public HttpResponseMessage UpdateUser(SecurityUserModel createdUser)
        {
            using (var httpClient = CreateHttpClient())
            {
                return
                    httpClient.PostAsJsonAsync(string.Format(Constants.Urls.UserAdminUserUri, createdUser.ObjectGuid),
                        createdUser).Result;
            }
        }
    }
}