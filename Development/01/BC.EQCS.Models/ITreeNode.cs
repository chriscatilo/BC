using System.Collections.Generic;

namespace BC.EQCS.Models
{
    public interface ITreeNode<TModel>
    {
        string Code { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string Parent { get; set; }
        bool IsActive { get; set; }
        
        IEnumerable<TModel> Children { get; set; }
    }
}