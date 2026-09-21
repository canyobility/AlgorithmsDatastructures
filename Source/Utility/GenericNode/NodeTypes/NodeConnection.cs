using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Utility.GenericNode
{
    public class NodeConnection<T>
    {
        public int Weight { get; set; }
        public Node<T>? Target { get; set; }

        public void Link(Node<T> targetNode, int weight)
        {
            this.Weight = weight;
            this.Target = targetNode;
        }
    }
}
