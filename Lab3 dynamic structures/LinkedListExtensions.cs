using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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

        //2.1 Функция для переноса первого элемента в конец
        public static void MoveFirstToLast<T>(this LinkedList<T> list)
        {
            if (list.head.Next == null)
            {
                return; // Список содержит только один элемент
            }

            Node<T> newHead = list.head.Next;

            list.tail.Next = list.head;
            list.head.Next = null;
            list.head = newHead;
        }

        //2.2 Функция для переноса последнего элемента в начало
        public static void MoveLastToFirst<T>(this LinkedList<T> list)
        {
            if (list.head.Next == null)
            {
                return; // Список содержит только один элемент
            }

            Node<T> current = list.head;
            Node<T> prev = null;

            while (current.Next != null)
            {
                prev = current;
                current = current.Next;
            }

            current.Next = list.head;
            list.head = current;
            prev.Next = null;
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

        //4. Функция, которая удаляет все неуникальные элементы
        public static void RemoveNonUniqueElements<T>(this LinkedList<T> list)
        {
            Node<T>? current = list.head;

            while (current != null)
            {
                Node<T> previous = current;
                Node<T>? checker = current.Next;

                while (checker != null)
                {
                    if (checker.Data!.Equals(current.Data))
                    {
                        previous.Next = checker.Next;
                        list.count--;
                    }
                    else
                    {
                        previous = checker;
                    }

                    checker = checker.Next;
                }

                current = current.Next;
            }
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

        //6. Функция, которая вставляет элемент в отсортированный по не убыванию список
        public static void InsertSorted<T>(this LinkedList<T> list, T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (list.head == null || Comparer<T>.Default.Compare(data, list.head.Data) <= 0)
            {
                newNode.Next = list.head;
                list.head = newNode;
                if (list.count == 0)
                    list.tail = list.head;
            }
            else
            {
                Node<T> current = list.head;
                Node<T> previous = null;

                while (current != null && Comparer<T>.Default.Compare(data, current.Data) > 0)
                {
                    previous = current;
                    current = current.Next;
                }

                newNode.Next = current;
                if (previous != null)
                    previous.Next = newNode;

                if (current == null)
                    list.tail = newNode;
            }

            list.count++;
        }

        // 7. Функция, которая удаляет из списка все элементы E, если таковые имеются
        public static void RemoveAll<T>(this LinkedList<T> list, T e)
        {
            while (list.Remove(e)) { }
        }

        //8. Функция, которая вставляеть элемент F перед элементом E
        public static void InsertBeforeFirstOccurrence<T>(this LinkedList<T> list, T F, T E)
        {
            Node<T> newNode = new Node<T>(F);
            Node<T>? current = list.head;
            Node<T>? previous = null;

            // Найти первое вхождение элемента E
            while (current != null && !current.Data.Equals(E))
            {
                previous = current;
                current = current.Next;
            }

            if (current != null)
            {
                if (previous == null)
                {
                    newNode.Next = list.head;
                    list.head = newNode;
                }
                else
                {
                    newNode.Next = current;
                    previous.Next = newNode;
                }

                list.count++;
            }
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

        //10. Функция, которая разделяет список на 2 по первому вхождению элемента target
        public static LinkedList<T> Split<T>(this LinkedList<T> list, T target)
        {
            Node<T>? current = list.head;
            Node<T>? previous = null;
            LinkedList<T> result = [];
            int count = 1;

            while (current != null)
            {
                if (current.Data!.Equals(target))
                {
                    if (count == 1)
                    {
                        result.head = current;
                        result.tail = list.tail;

                        list.head = null;
                        list.tail = null;

                        result.count = list.count;
                        list.count = 0;

                        break;
                    }

                    result.head = current;

                    if (previous != null)
                    {
                        previous.Next = null;
                        result.tail = list.tail;
                        list.tail = previous;
                    }
                    else
                    {
                        result.tail = current;
                    }

                    result.count = count;
                    list.count -= count;

                    break;
                }

                previous = current;
                current = current.Next;
                count++;
            }

            return result;
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

        //12. Функция, которая меняет местами два элемента списка
        public static void SwapElements<T>(this LinkedList<T> list, T element1, T element2)
        {
            if (list.head == null || list.head.Next == null)
            {
                return; // Список пуст или содержит только один элемент
            }

            Node<T>? prev1 = null;
            Node<T>? curr1 = list.head;
            Node<T>? prev2 = null;
            Node<T>? curr2 = list.head;

            while (curr1 != null && !curr1.Data.Equals(element1))
            {
                prev1 = curr1;
                curr1 = curr1.Next;
            }

            while (curr2 != null && !curr2.Data.Equals(element2))
            {
                prev2 = curr2;
                curr2 = curr2.Next;
            }

            if (curr1 == null || curr2 == null)
            {
                return;
            }

            if (curr1.Next == curr2)
            {
                curr1.Next = curr2.Next;
                curr2.Next = curr1;
                if (prev1 != null)
                {
                    prev1.Next = curr2;
                }
                else
                {
                    list.head = curr2;
                }
            }
            else if (curr2.Next == curr1)
            {
                curr2.Next = curr1.Next;
                curr1.Next = curr2;
                if (prev2 != null)
                {
                    prev2.Next = curr1;
                }
                else
                {
                    list.head = curr1;
                }
            }
            else
            {
                Node<T> temp = curr1.Next;
                curr1.Next = curr2.Next;
                curr2.Next = temp;

                if (prev1 != null)
                {
                    prev1.Next = curr2;
                }
                else
                {
                    list.head = curr2;
                }

                if (prev2 != null)
                {
                    prev2.Next = curr1;
                }
                else
                {
                    list.head = curr1;
                }
            }

            if (curr1 == list.tail)
            {
                list.tail = curr2;
            }
            else if (curr2 == list.tail)
            {
                list.tail = curr1;
            }
        }
    }
}