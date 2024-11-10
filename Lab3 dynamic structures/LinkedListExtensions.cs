using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_dynamic_structures
{
    public static class LinkedListExtensions
    {
        // 1. Функция, которая переворачивает список L
        public static void Reverse<T>(this LinkedList<T> list)
        {
            if (list.Count <= 1) return;

            Node<T> current = list.head;
            Node<T> prev = null;
            Node<T> next = null;

            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            list.tail = list.head;
            list.head = prev;
        }


        // 3. Функция, которая определяет количество различных элементов списка
        public static int CountDistinct(this LinkedList<int> list)
        {
            HashSet<int> distinctElements = new HashSet<int>();
            Node<int> current = list.head;
            while (current != null)
            {
                distinctElements.Add(current.Data);
                current = current.Next;
            }
            return distinctElements.Count;
        }

        // 5. Функция вставки списка самого в себя после первого вхождения числа х
        public static void InsertIntoItself<T>(this LinkedList<T> list, T x)
        {
            Node<T> nodeX = null;
            Node<T> current = list.head;

            while (current != null)
            {
                if (current.Data.Equals(x))
                {
                    nodeX = current;
                    break;
                }
                current = current.Next;
            }

            if (nodeX != null)
            {
                LinkedList<T> copy = new LinkedList<T>();
                current = list.head;
                while (current != null)
                {
                    copy.Add(current.Data);
                    current = current.Next;
                }


                Node<T> nextAfterX = nodeX.Next;
                current = copy.head;

                while (current != null)
                {
                    nodeX.Next = new Node<T>(current.Data);
                    nodeX = nodeX.Next;
                    current = current.Next;

                }
                nodeX.Next = nextAfterX;

                while (nextAfterX != null)
                {
                    list.tail = nextAfterX;
                    nextAfterX = nextAfterX.Next;

                }

            }
        }


        // 7. Функция, которая удаляет из списка все элементы E, если таковые имеются
        public static void RemoveAll<T>(this LinkedList<T> list, T e)
        {
            while (list.Remove(e)) { }
        }


        // 9. Функция, которая дописывает к списку L список E
        public static void AppendList<T>(this LinkedList<T> listL, LinkedList<T> listE)
        {
            if (listE.head != null)
            {
                if (listL.head == null)
                {
                    listL.head = listE.head;
                    listL.tail = listE.tail;
                }

                else
                {
                    listL.tail.Next = listE.head;
                    listL.tail = listE.tail;
                }


                listL.count += listE.Count;
            }

        }


        // 11. Функция, которая удваивает список, приписывая его к самому себе
        public static void Double<T>(this LinkedList<T> list)
        {
            LinkedList<T> copy = new LinkedList<T>();

            Node<T> current = list.head;
            while (current != null)
            {
                copy.Add(current.Data);
                current = current.Next;
            }

            list.AppendList(copy);


        }
    }
}