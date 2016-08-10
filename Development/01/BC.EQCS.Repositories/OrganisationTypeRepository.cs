using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class OrganisationTypeRepository: Repository<OrganisationType, OrganisationTypeModel>
    {
        public OrganisationTypeRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }

        public override OrganisationTypeModel GetByUniqueCode(string code)
        {
            var entity = Context.OrganisationTypes
                .Include(type => type.Organisations)
                .FirstOrDefault(
                    orgType => orgType.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));

            var model = Mapper.Map<OrganisationTypeModel>(entity);

            return model;
        }

        public override IEnumerable<OrganisationTypeModel> GetAll()
        {
            return Context.Set<OrganisationType>()
                .Include("Organisations")
                .Select(Mapper.Map<OrganisationTypeModel>);
        }
    }
}
