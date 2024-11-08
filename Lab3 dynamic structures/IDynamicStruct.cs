using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dynamic_structures
{
    public interface IDynamicStruct<T>
    {
        void Push(T elem);
        T Pop();

        T Peek();

        bool IsEmpty();

        void Print();
    }
}
