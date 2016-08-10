using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Extensions;

namespace BC.EQCS.Repositories
{
    public abstract class TreeRepository<TEntity, TModel> : Repository<TEntity, TModel>, ITreeRepository<TModel>
        where TModel : class, ITreeNode<TModel>
        where TEntity : class
    {
        protected TreeRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public TModel GetTreeByNodeCodes(params string[] codes)
        {
            var nodes = GetNodesByCodes(codes);

            var lookup = nodes.ToDictionary(node => node.Key);

            foreach (var item in lookup.Values.OrderBy(i => i.ParentKey).ThenBy(i => i.Key))
            {
                if (item.ParentKey == null) continue;

                var parentContainer = lookup[item.ParentKey ?? 0];
                item.Node.Parent = parentContainer.Node.Code;
                parentContainer.Children.Add(item.Node);
            }

            foreach (var container in lookup.Values)
            {
                container.Node.Children = container.Children.AsReadOnly();
            }

            return lookup.Values.Any() ? lookup.Values.First(item => item.ParentKey == null).Node : default(TModel);
        }


        public override TModel GetByUniqueCode(string code)
        {
            var rootNode = GetTreeByNodeCodes(code);
            return rootNode == null ? null : rootNode.FindByCode(code);
        }

        public abstract IEnumerable<NodeContainer> GetNodesByCodes(params string[] codes);


        public ICollection<TModel> GetAllAncestorsOfNodeByCode(string code)
        {
            var nodes = GetNodesByCodes(code).Select(n => n.Node);
            return nodes.ToList();
        }


        public class NodeContainer
        {
            public NodeContainer()
            {
                Children = new List<TModel>();
            }

            public int Key { get; set; }
            public int? ParentKey { get; set; }
            public TModel Node { get; set; }
            public List<TModel> Children { get; private set; }
        }
    }
}