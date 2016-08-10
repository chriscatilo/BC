using BC.EQCS.Entities.Models;

namespace BC.EQCS.Entities
{
    public class EntityFactory : IEntityFactory
    {
        private readonly object _lockObject = new object();
        
        private EqcsEntities _entities;

        public EqcsEntities Create()
        {
            if (_entities != null)
            {
                return _entities;
            }

            lock (_lockObject)
            {
                if (_entities != null)
                {
                    return _entities;
                }

                _entities = new EqcsEntities();

                _entities.Configuration.LazyLoadingEnabled = false;

                return _entities;
            }
        }

        public void Dispose()
        {
            if (_entities.ChangeTracker.HasChanges())
            {
                _entities.SaveChanges();
            }
        }
    }
}