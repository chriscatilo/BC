using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class TestLocationRepository : Repository<TestLocation, TestLocationModel>
    {
        public TestLocationRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override TestLocationModel GetByUniqueCode(string code)
        {
            var entity = Context.TestLocations
                .IncludeAllNavigationProperties(Context)
                .FirstOrDefault(obj => obj.AdminUnit.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));

            var model = Mapper.Map<TestLocationModel>(entity);

            return model;
        }

        public override IEnumerable<TestLocationModel> GetAll()
        {
            var values = Context.TestLocations
                .IncludeAllNavigationProperties(Context)
                .Select(Mapper.Map<TestLocationModel>);

            return values;
        }
    }
}