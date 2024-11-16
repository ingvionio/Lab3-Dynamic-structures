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
            List<int> inOrderList = new List<int>();
            InOrderTraversalToList(Root, inOrderList);
            int[] array = inOrderList.ToArray();
            BuildMaxHeap(array);
            Root = BuildTreeFromArray(array, 0, array.Length);
        }
        //
        private void InOrderTraversalToList(TreeNode node, List<int> list)
        {
            if (node != null)
            {
                InOrderTraversalToList(node.Left, list);
                list.Add(node.Value);
                InOrderTraversalToList(node.Right, list);
            }
        }
        //
        private void BuildMaxHeap(int[] array)
        {
            int n = array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                HeapifyDown(array, n, i);
            }
        }
        //
        private void HeapifyDown(int[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && array[left] > array[largest])
            {
                largest = left;
            }

            if (right < n && array[right] > array[largest])
            {
                largest = right;
            }

            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                HeapifyDown(array, n, largest);
            }
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
            TreeNodeToList(Root, sortedList);
            int[] array = sortedList.ToArray();
            BuildMaxHeap(array);

            for (int i = array.Length - 1; i > 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                HeapifyDown(array, i, 0);
            }

            foreach (int value in array)
            {
                Console.Write(value + " ");
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
