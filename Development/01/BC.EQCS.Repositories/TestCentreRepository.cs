using System;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class TestCentreRepository : Repository<TestCentre, TestCentreModel>
    {
        public TestCentreRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override TestCentreModel GetByUniqueCode(string code)
        {
            var entity = Context
                .TestCentres
                .FirstOrDefault(centre => centre.CentreNumber.Equals(code, StringComparison.InvariantCultureIgnoreCase));

            var model = Mapper.Map<TestCentreModel>(entity);

            return model;
        }
    }
}