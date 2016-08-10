using System.Collections.Generic;

namespace BC.EQCS.Models
{
    public class AdminUnitModel : ITreeNode<AdminUnitModel>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Parent { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<AdminUnitModel> Children { get; set; }
    }

}
