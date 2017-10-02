using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peyton.Core.Repository;

namespace Peyton.Core.Extensions
{
    public class TreeNode : Entity
    {
        public TreeNode Parent { get; set; }
        public List<TreeNode> Nodes { get; set; }
        public string Text { get; set; }
    }
}
