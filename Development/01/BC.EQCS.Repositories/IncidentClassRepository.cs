using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Models.Extensions;
using BC.EQCS.Repositories.Utils;

namespace BC.EQCS.Repositories
{
    public class IncidentClassRepository : TreeRepository<IncidentClass, IncidentClassModel>
    {
        public IncidentClassRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override IEnumerable<NodeContainer> GetNodesByCodes(params string[] codes)
        {
            var codesParam = SqlHelper.CreateCodesSqlParameter(codes);

            const string query = "SELECT * FROM [dbo].[SelectIncidentClassTree](@codes)";

            var dbQuery = Context.Database.SqlQuery<QueryResult>(query, codesParam);

            var containers = dbQuery.Select(item => new NodeContainer
            {
                Key = item.Id,
                ParentKey = item.ParentId,
                Node = new IncidentClassModel
                {
                    Code = item.Code,
                    Name = item.Name,
                    RiskRatingDefault = item.RiskRatingDefault,
                    ResidualRiskRatingDefault = item.ResidualRiskRatingDefault,
                    Type = item.TypeCode,
                    UkviImmediateReportType = item.UkviImmediateReportType,
                    IsActive = item.IsActive
                }
            }).ToList();

            return containers;
        }

        public override IncidentClassModel GetByUniqueCode(string code)
        {
            var rootNode = GetTreeByNodeCodes(code);
            return rootNode == null ? null : rootNode.FindByCode(code);
        }

        protected class QueryResult
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public int? ParentId { get; set; }
            public string TypeCode { get; set; }
            public bool IsActive { get; set; }
            public string RiskRatingDefault { get; set; }
            public string ResidualRiskRatingDefault { get; set; }
            public string UkviImmediateReportType { get; set; }
        }
    }
}