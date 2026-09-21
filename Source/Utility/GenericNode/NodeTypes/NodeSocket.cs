using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Utility.GenericNode
{
    public class NodeSocket<T>
    {
        public int ID { get; internal set; }
        public NodeConnection<T>? Value { get; internal set; }
        public bool isConnected => Value != null;
    }
}
