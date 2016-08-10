using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;

namespace BC.EQCS.Repositories
{
    public abstract class Repository<TEntity, TModel> : IRepository<TModel>
        where TEntity : class
        where TModel : class
    {
        private readonly IEntityFactory _entityFactory;

        protected Repository(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        protected EqcsEntities Context
        {
            get { return _entityFactory.Create(); }
        }

        protected Func<TEntity, int> KeyValue { get; set; }

        public Type ForType
        {
            get { return typeof (TModel); }
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            return Context.Set<TEntity>().ToList().Select(Mapper.Map<TModel>);
        }

        public virtual TModel GetById(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            if (entity == null)
            {
                return null;
            }
            return Mapper.Map<TModel>(entity);
        }

        public virtual TModel GetByUniqueCode(string code)
        {
            throw new NotImplementedException();
        }

        public virtual int Create(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);

            Context.Set<TEntity>().Add(entity);

            Context.SaveChanges();

            return KeyValue != null ? KeyValue(entity) : 0;
        }

        public virtual void Update(TModel value)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            return entity != null;
        }

        public void StartTransaction()
        {
            Context.Database.BeginTransaction();
        }

        public bool TransactionStarted()
        {
            return Context.Database.CurrentTransaction != null;
        }

        public DbContextTransaction GetTransaction()
        {
            return Context.Database.CurrentTransaction;
        }
    }
}