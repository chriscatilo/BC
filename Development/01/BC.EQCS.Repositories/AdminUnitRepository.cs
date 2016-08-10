using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Repositories.Utils;

namespace BC.EQCS.Repositories
{
    public class AdminUnitRepository : TreeRepository<AdminUnit, AdminUnitModel>
    {
        public AdminUnitRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override IEnumerable<NodeContainer> GetNodesByCodes(params string[] codes)
        {
            var codesParam = SqlHelper.CreateCodesSqlParameter(codes);

            const string query = "SELECT * FROM [dbo].[SelectAdminUnitTree](@codes)";

            var dbQuery = Context.Database.SqlQuery<QueryResult>(query, codesParam);

            var containers = dbQuery.Select(item => new NodeContainer
            {
                Key = item.Id,
                ParentKey = item.ParentId,
                Node = new AdminUnitModel
                {
                    Code = item.Code,
                    Name = item.Name,
                    Type = item.TypeCode,
                    IsActive = item.IsActive
                }
            }).ToList();

            return containers;
        }

        [Cache]
        public override AdminUnitModel GetByUniqueCode(string code)
        {
           return base.GetByUniqueCode(code);
        }

        protected class QueryResult
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public int? ParentId { get; set; }
            public string TypeCode { get; set; }
            public bool IsActive { get; set; }
        }
    }
}