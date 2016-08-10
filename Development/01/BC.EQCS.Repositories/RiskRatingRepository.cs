using System;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class RiskRatingRepository : Repository<RiskRating, RiskRatingModel>
    {
        public RiskRatingRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
            
        }

        public override RiskRatingModel GetByUniqueCode(string code)
        {
            var entity = Context.RiskRatings.FirstOrDefault(
                rating => rating.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));

            var model = Mapper.Map<RiskRatingModel>(entity);

            return model;
        }
    }
}
