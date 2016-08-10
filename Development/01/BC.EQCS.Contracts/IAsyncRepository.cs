using System.Collections.Generic;
using System.Threading.Tasks;

namespace BC.EQCS.Contracts
{
    public interface IAsyncRepository<TModel>
    {
        Task<IEnumerable<TModel>> GetAll();

        Task<TModel> GetById(int id);

        Task<TModel> GetByUniqueCode(string code);

        Task<int> Create(TModel value);

        Task Update(TModel value);

        Task Delete(TModel value);

        Task<bool> Exists(string code);
    }
}