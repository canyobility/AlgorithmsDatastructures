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

        // Have used indexers in Lua & Python, but this was my first using C#. 
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/
        public NodeSocket<T> this[int index]
        {
            get { return this.GetSocket(index); }
        }


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

        /// <summary>
        /// Used to connect to another node. 
        /// </summary>
        /// <param name="targetNode"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public NodeConnection<T> Connect(Node<T> targetNode, int weight = 1)
        {
            // Find avalable socket
            NodeSocket<T> targetSocket = this.GetEmptySocket();

            NodeConnection<T> connection = new NodeConnection<T>();
            connection.Target = targetNode;

            targetSocket.Connection = connection;
            connection.Weight = weight;

            return connection;
        }

        /// <summary>
        /// <para>Doubly links the current node to the target node by creating two connection instances.</para>
        /// <para>Unlike the standard connect method, which only supports A → B connections, this method connects A → B AND A ← A.</para>
        /// </summary>
        /// <param name="targetNode"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (NodeConnection<T> fromConnection, NodeConnection<T> toConnection) ConnectBidirectional(Node<T> targetNode, int weight)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the current 
        /// </summary>
        /// <param name="connection"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Disconnect(NodeConnection<T> connection)
        {
            throw new NotImplementedException();
            
        }


        public NodeSocket<T> GetSocket(int ID)
        {
            if (ID < 0 || ID >= this.sockets.Count)
                throw new ArgumentOutOfRangeException();

            return this.sockets[ID];
        }
        public NodeConnection<T> GetAtSocket(int ID)
        { return this.GetAtSocket(ID); }


        /// <summary>
        /// Searches for an empty socket. If no socket is found, a socket will be created.
        /// </summary>
        /// <returns>NodeSocket: Empty node socket.</returns>
        protected NodeSocket<T> GetEmptySocket()
        {
            foreach (NodeSocket<T> socket in this)
            {
                if (socket.Connection is null)
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
