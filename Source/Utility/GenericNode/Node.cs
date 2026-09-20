using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Utility.GenericNode
{
    public class Node<T>
    {
        public T value { get; set; }

        public Node(T var)
        {
            value = var;
        }

    }
}
