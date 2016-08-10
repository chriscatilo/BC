using System;
using System.Linq;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Repositories
{
    public class ProductRepository : Repository<Product, ProductModel>
    {
        public ProductRepository(IEntityFactory entityFactory)
            : base(entityFactory)
        {
        }
        public override ProductModel GetByUniqueCode(string code)
        {
            var entity = Context.Products.FirstOrDefault(
                rating => rating.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));

            var model = Mapper.Map<ProductModel>(entity);

            return model;
        }
    }
}
