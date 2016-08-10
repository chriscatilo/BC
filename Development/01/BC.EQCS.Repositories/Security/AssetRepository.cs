using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories.Security
{
    public class AssetRepository: Repository<ApplicationAsset, Asset>
    {
        public AssetRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
            base.KeyValue = asset => asset.Id;
        }
    }
}
