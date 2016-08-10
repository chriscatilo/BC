using System;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class CountryRepository : Repository<Country, CountryModel>
    {
        public CountryRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override CountryModel GetByUniqueCode(string code)
        {
            var entity =
                Context.Countries.FirstOrDefault(
                    country => country.IsoCode.Equals(code, StringComparison.InvariantCultureIgnoreCase));

            var model = Mapper.Map<CountryModel>(entity);

            return model;
        }
    }
}