using System;
using BC.EQCS.Entities.Models;

namespace BC.EQCS.Entities
{
    public interface IEntityFactory : IDisposable
    {
        EqcsEntities Create();
    }
}