using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Utility.GenericNode
{
    public class NodeSocket<T>
    {
        public int ID { get; internal set; }
        public NodeConnection<T>? Connection { get; internal set; }
        public bool isConnected => Connection != null;

        /// <summary>
        /// Shorthand to this.Connection.Target
        /// </summary>
        public Node<T>? Target 
        {
            get
            {
                if (isConnected is false) return null;
                return this.Connection!.Target;
            }
        }
    }
}
