using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cours_Task5_Tree
{
    internal class BinaryTree
    {
        internal BinaryTreeNode? _MainRoot;
        public int Height
        {
            get => _MainRoot.Height;
        }
        public BinaryTree(IEnumerable collection) 
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            if (collection is ICollection c)
            {
                int count = c.Count;
                if (count == 0)
                {
                    _MainRoot = null;
                }
                else
                {
                    foreach (int item in c)
                    {
                        if (_MainRoot == null)
                        {
                            _MainRoot = new BinaryTreeNode(item);
                        }
                        else
                        {
                            AddNode(_MainRoot, new BinaryTreeNode(item));
                        }
                    }
                }
            }
        }
        private void AddNode(BinaryTreeNode parent, BinaryTreeNode child)
        {
            if (parent.Value == child.Value)
            {
                return;
            }
            
            if (child.Value > parent.Value)
            {
                if (parent.RightChildNode == null)
                {
                    child.ParentNode = parent;
                    parent.RightChildNode = child;
                }
                else
                {
                    AddNode(parent.RightChildNode, child);
                    
                }    
            }
            else
            {
                if (parent.LeftChildNode == null)
                {
                    
                    child.ParentNode = parent;
                    parent.LeftChildNode = child;
                }
                else
                {
                    AddNode(parent.LeftChildNode, child);
                }
            }
            BinaryTreeNode Left = parent.LeftChildNode;
            BinaryTreeNode Right = parent.RightChildNode;
            int LeftHeight = Left != null ? Left.Height : 0;
            int RightHeight = Right != null ? Right.Height : 0;
            parent.Height = Math.Max(LeftHeight, RightHeight) + 1;

        }
    }
    class BinaryTreeNode
    { 
        public int Value { get; private set; }
        internal int Height;
        internal BinaryTreeNode LeftChildNode;
        internal BinaryTreeNode RightChildNode;
        internal BinaryTreeNode ParentNode;
        public BinaryTreeNode(int value)
        {
            Height = 1;
            this.Value = value;
            LeftChildNode = null; 
            RightChildNode = null; 
            ParentNode = null;
        }
    }
}
