using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_dynamic_structures
{
    internal class BrowserHistory
    {
        MyStack<string> backStack = new MyStack<string>();
        MyStack<string> forwardStack = new MyStack<string>();

        // Constructor to initialize object with homepage
        public BrowserHistory(string homepage)
        {
            backStack.Push(homepage);
        }

        // Visit current url
        public void Visit(string url)
        {
            while (forwardStack.Count > 0)
            {
                forwardStack.Pop();
            }
            backStack.Push(url);
        }

        // 'steps' move backward in history and return
        // current page
        public string Back(int steps)
        {
            while (backStack.Count > 1 && steps-- > 0)
            {
                forwardStack.Push(backStack.Peek());
                backStack.Pop();
            }
            return backStack.Peek();
        }

        // 'steps' move forward and return current page
        public string Forward(int steps)
        {
            while (forwardStack.Count > 0 && steps-- > 0)
            {
                backStack.Push(forwardStack.Peek());
                forwardStack.Pop();
            }
            return backStack.Peek();
        }

        public string Current()
        {
            return backStack.IsEmpty() ? null : backStack.Peek();
        }
    }
}
