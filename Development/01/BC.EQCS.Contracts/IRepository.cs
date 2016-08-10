using System.Collections.Generic;
using System.Data.Entity;

namespace BC.EQCS.Contracts
{
    public interface IRepository<TModel>
    {
        IEnumerable<TModel> GetAll();
        TModel GetById(int id);
        TModel GetByUniqueCode(string code);
        int Create(TModel value);
        void Update(TModel value);
        void Delete(int id);
        bool Exists(int id);
        void StartTransaction();
        bool TransactionStarted();
        DbContextTransaction GetTransaction();
    }
}