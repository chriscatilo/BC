using System;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class UkviImmediateReportTypeRepository : Repository<UkviImmediateReportType, UkviImmediateReportTypeModel>
    {
        public UkviImmediateReportTypeRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override UkviImmediateReportTypeModel GetByUniqueCode(string code)
        {
            var entity =
                Context.UkviImmediateReportTypes.FirstOrDefault(
                    reportType => reportType.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));

            var model = Mapper.Map<UkviImmediateReportTypeModel>(entity);

            return model;
        }
    }
}