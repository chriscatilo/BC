//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Linq;
//using BC.EQCS.DataTransfer;
//using BC.EQCS.Models;
//using BC.EQCS.Utils;

//namespace BC.EQCS.Repositories.Utils
//{
//    public static class TreeNodeExtensions
//    {
//        public static TNode FindByCode<TNode>(this TNode node, string code)
//            where TNode : ITreeNode<TNode>
//        {
//            if (code.EqualsCaseInsensitive(node.Code))
//            {
//                return node;
//            }

//            if (node.Children == null || !node.Children.Any())
//            {
//                return default(TNode);
//            }

//            var value = node.Children
//                .Select(child => child.FindByCode(code))
//                .FirstOrDefault(unit => unit != null);

//            return value;
//        }

//        public static IEnumerable<TNode> GetByType<TNode>(this TNode node, string type)
//            where TNode : ITreeNode<TNode>
//        {
//            var list = new List<TNode>();

//            node.FillByType(type, list);

//            return list.AsReadOnly();
//        }

//        private static void FillByType<TNode>(this TNode parent, string type, List<TNode> list)
//            where TNode : ITreeNode<TNode>
//        {
//            if (type.EqualsCaseInsensitive(parent.Type))
//            {
//                list.Add(parent);
//                return;
//            }

//            if (parent.Children == null || !parent.Children.Any())
//            {
//                return;
//            }

//            foreach (var child in parent.Children)
//            {
//                FillByType(child, type, list);
//            }
//        }

//        public static TNode FindByCodeActiveOnly<TNode>(this TNode node, string code)
//            where TNode : ITreeNode<TNode>
//        {
//            if (code.EqualsCaseInsensitive(node.Code))
//            {
//                var clone = node.CloneActiveOnly();

//                return clone;
//            }

//            if (node.Children == null || !node.Children.Any())
//            {
//                return default(TNode);
//            }

//            var value = node.Children.Select(child => child.FindByCodeActiveOnly(code)).FirstOrDefault(unit => unit != null);

//            return value;
//        }

//        private static TNode CloneActiveOnly<TNode>(this TNode node) where TNode : ITreeNode<TNode>
//        {
//            if (!node.IsActive)
//            {
//                return default(TNode);
//            }

//            var clone = Mapper.Map<TNode>(node);
//            clone.Parent = node.Parent;

//            if (node.Children == null || !node.Children.Any())
//            {
//                return clone;
//            }

//            clone.Children = node.Children.Select(child => child.TraverseForActiveNodes()).Where(item => item != null);

//            return clone;
//        }

//        private static TNode Clone<TNode>(this TNode node) where TNode : ITreeNode<TNode>
//        {
//            var clone = Mapper.Map<TNode>(node);
//            clone.Parent = node.Parent;
//            return clone;
//        }

//        private static TNode TraverseForActiveNodes<TNode>(this TNode node)
//            where TNode : ITreeNode<TNode>
//        {
//            if (!node.IsActive)
//            {
//                return default(TNode);
//            }

//            var clone = node.Clone();

//            clone.Children = node.Children.Select(child => child.TraverseForActiveNodes()).Where(item => item != null);

//            return clone;

//        }

//        public static IEnumerable<TNode> GetByTypeActiveOnly<TNode>(this TNode node, string type)
//            where TNode : ITreeNode<TNode>
//        {
//            var list = new List<TNode>();

//            node.FillByTypeActiveOnly(type, list);

//            return list.AsReadOnly();
//        }

//        private static void FillByTypeActiveOnly<TNode>(this TNode node, string type, List<TNode> list)
//            where TNode : ITreeNode<TNode>
//        {
//            if (type.EqualsCaseInsensitive(node.Type))
//            {
//                var clone = node.CloneActiveOnly();

//                if (clone != null)
//                {
//                    list.Add(clone);
//                }

//                return;
//            }

//            if (node.Children == null || !node.Children.Any())
//            {
//                return;
//            }

//            foreach (var child in node.Children)
//            {
//                child.FillByTypeActiveOnly(type, list);
//            }
//        }
//    }
//}
