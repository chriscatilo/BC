using System.Collections.Generic;
using BC.EQCS.Models;

namespace BC.EQCS.Contracts
{
    public interface ITreeRepository<TModel> : IRepository<TModel>
        where TModel : ITreeNode<TModel>
    {
        TModel GetTreeByNodeCodes(params string[] codes);
        ICollection<TModel> GetAllAncestorsOfNodeByCode(string code);
    }
}