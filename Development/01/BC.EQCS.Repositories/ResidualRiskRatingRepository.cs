using System;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class ResidualRiskRatingRepository : Repository<ResidualRiskRating, ResidualRiskRatingModel>
    {
        public ResidualRiskRatingRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
            
        }

        public override ResidualRiskRatingModel GetByUniqueCode(string code)
        {
            var entity = Context.ResidualRiskRatings.FirstOrDefault(
                rating => rating.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));

            var model = Mapper.Map<ResidualRiskRatingModel>(entity);

            return model;
        }
    }
}
