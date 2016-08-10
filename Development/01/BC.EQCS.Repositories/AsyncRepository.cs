using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;

namespace BC.EQCS.Repositories
{
    public abstract class AsyncRepository<TEntity, TModel> : IAsyncRepository<TModel>
        where TEntity : class
        where TModel : class
    {
        private readonly IEntityFactory _entityFactory;

        protected EqcsEntities Context
        {
            get { return _entityFactory.Create(); }
        }

        protected AsyncRepository(IEntityFactory entityFactory, IModelValidator<TModel> validator)
            : this(entityFactory)
        {
            Validator = validator;
        }

        protected AsyncRepository(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            var all = await Context.Set<TEntity>().ToListAsync();
            return all.Select(Mapper.Map<TModel>);
        }

        public virtual async Task<TModel> GetById(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return Mapper.Map<TModel>(entity);
        }

        public abstract Task<TModel> GetByUniqueCode(string code);

        public virtual async Task<int> Create(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);

            Context.Set<TEntity>().Add(entity);

            await Context.SaveChangesAsync();

            return KeyValue != null ? KeyValue(entity) : 0;
        }

        public abstract Task Update(TModel model);

        public abstract Task Delete(TModel model);

        public abstract Task<bool> Exists(string code);

        public IModelValidator<TModel> Validator { get; private set; }

        protected Func<TEntity, int> KeyValue { get; set; }

        public Type ForType
        {
            get { return typeof(TModel); }
        }

    }
}