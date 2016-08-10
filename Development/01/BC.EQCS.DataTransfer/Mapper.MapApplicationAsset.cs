using BC.EQCS.Entities.Models;
using BC.EQCS.Security.Models;

namespace BC.EQCS.DataTransfer
{
    public static partial class Mapper
    {
        private static void MapApplicationAsset()
        {
            AutoMapper.Mapper.CreateMap<ApplicationAsset, Asset>();
        }
    }
}