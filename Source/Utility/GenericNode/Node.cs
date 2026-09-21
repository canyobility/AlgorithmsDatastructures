using Source.Utility.GenericNode.NodeTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Source.Utility.GenericNode
{
    public class Node<T> : IEnumerable where T : notnull
    {
        public T value { get; set; }
        public List<NodeSocket<T>> sockets;

        public Node(T var )
        {
            value = var;
            sockets = new List<NodeSocket<T>>();
        }

        public Node() : this(default(T)!) 
        {   }


        public NodeSocket<T> AddSocket() 
        { 
            NodeSocket<T> socket = new NodeSocket<T>();
            socket.ID = sockets.Count;
            sockets.Add(socket);
            return socket;
        }

        public NodeConnection<T> Connect(Node<T> targetNode, int weight = 1)
        {
            // Find avalable socket
            NodeSocket<T> targetSocket = this.GetEmptySocket();

            NodeConnection<T> connection = new NodeConnection<T>();
            connection.Target = targetNode;

            targetSocket.Value = connection;
            connection.Weight = weight;

            return connection;
        }

        public void Disconnect(NodeConnection<T> connection)
        {
            throw new NotImplementedException();
            
        }

        public NodeConnection<T> GetAtSocket(int ID)
        {
            if (ID < 0 || ID >= this.sockets.Count)
                throw new ArgumentOutOfRangeException();

            return this.sockets[ID].Value!;
        }

        protected NodeSocket<T>? GetEmptySocket()
        {
            foreach (NodeSocket<T> socket in this)
            {
                if (socket.Value is null)
                {
                    return socket;
                }
            }

            return this.AddSocket();
        }

        public IEnumerator GetEnumerator()
        {
            foreach (NodeSocket<T> socket in this.sockets)
            {
                yield return socket;
            }
        }

    }
}
