using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dynamic_structures
{
    public class BinaryTree
    {
        public TreeNode Root { get; private set; }

        public BinaryTree()
        {
            Root = null;
        }

        public void Insert(int value)
        {
            Root = InsertRecursive(Root, value);
        }

        private TreeNode InsertRecursive(TreeNode node, int value)
        {
            if (node == null)
            {
                return new TreeNode(value);
            }

            if (value < node.Value)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = InsertRecursive(node.Right, value);
            }

            return node;
        }

        public void Heapify()
        {
            HeapifyDown(Root);
        }
        //
        
        private TreeNode BuildTreeFromArray(int[] array, int index, int length)
        {
            if (index >= length)
            {
                return null;
            }

            TreeNode node = new TreeNode(array[index]);
            node.Left = BuildTreeFromArray(array, 2 * index + 1, length);
            node.Right = BuildTreeFromArray(array, 2 * index + 2, length);

            return node;
        }

        public void HeapSortTraversal()
        {
            List<int> sortedList = new List<int>();
            Heapify();
            while (Root != null)
            {
                int maxValue = ExtractMax();
                sortedList.Add(maxValue);
            }

            foreach (int value in sortedList)
            {
                Console.Write(value + " ");
            }
        }

        private int ExtractMax()
        {
            if (Root == null)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            int maxValue = Root.Value;
            Root = RemoveRoot(Root);
            return maxValue;
        }

        private TreeNode RemoveRoot(TreeNode root)
        {
            if (root.Left == null && root.Right == null)
            {
                return null;
            }

            if (root.Left == null)
            {
                return root.Right;
            }

            if (root.Right == null)
            {
                return root.Left;
            }

            TreeNode lastNode = GetLastNode(root);
            root.Value = lastNode.Value;
            RemoveLastNode(root, lastNode);
            HeapifyDown(root);

            return root;
        }

        private TreeNode GetLastNode(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            TreeNode lastNode = null;
            while (queue.Count > 0)
            {
                lastNode = queue.Dequeue();
                if (lastNode.Right != null)
                {
                    queue.Enqueue(lastNode.Right);
                }
                if (lastNode.Left != null)
                {
                    queue.Enqueue(lastNode.Left);
                }
            }

            return lastNode;
        }

        private void RemoveLastNode(TreeNode root, TreeNode lastNode)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            TreeNode parent = null;
            bool isLeftChild = false;

            while (queue.Count > 0)
            {
                TreeNode current = queue.Dequeue();
                if (current.Left == lastNode)
                {
                    parent = current;
                    isLeftChild = true;
                    break;
                }
                if (current.Right == lastNode)
                {
                    parent = current;
                    isLeftChild = false;
                    break;
                }

                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
            }

            if (parent != null)
            {
                if (isLeftChild)
                {
                    parent.Left = null;
                }
                else
                {
                    parent.Right = null;
                }
            }
        }

        private void HeapifyDown(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            int largestValue = root.Value;
            TreeNode largestNode = root;

            if (root.Left != null && root.Left.Value > largestValue)
            {
                largestValue = root.Left.Value;
                largestNode = root.Left;
            }

            if (root.Right != null && root.Right.Value > largestValue)
            {
                largestValue = root.Right.Value;
                largestNode = root.Right;
            }

            if (largestNode != root)
            {
                int temp = root.Value;
                root.Value = largestNode.Value;
                largestNode.Value = temp;
                HeapifyDown(largestNode);
            }
        }
        //
        private void TreeNodeToList(TreeNode node, List<int> list)
        {
            if (node != null)
            {
                list.Add(node.Value);
                TreeNodeToList(node.Left, list);
                TreeNodeToList(node.Right, list);
            }
        }

        public void DFSTraversal()
        {
            Heapify();
            DFSTraversalRecursive(Root);
        }

        private void DFSTraversalRecursive(TreeNode node)
        {
            if (node != null)
            {
                Console.Write(node.Value + " ");
                DFSTraversalRecursive(node.Left);
                Console.Write("* ");
                DFSTraversalRecursive(node.Right);
            }
        }
    }
}
