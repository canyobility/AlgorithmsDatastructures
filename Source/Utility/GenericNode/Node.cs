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


        public NodeSocket<T> AddSocket(int Id) 
        { 
            NodeSocket<T> socket = new NodeSocket<T>();
            socket.ID = Id;
            sockets.Add(socket);
            return socket;
        }

        /// <summary>
        /// <para>Overload of the AddSocket method which attempts to find a new ID to give
        /// your port automatically.</para>
        /// <para>Recomend assigning your ports manually to prevent
        /// unessessary computations. This is mainly offered for backwards compatability
        /// from my previous code.</para>
        /// </summary>
        /// <returns></returns>
        public NodeSocket<T> AddSocket() => AddSocket(this.GetAutomatedId());


        /// <summary>
        /// Used to connect to another node. 
        /// </summary>
        /// <param name="targetNode"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public NodeConnection<T> Connect(Node<T> targetNode, int portId, int weight = 1)
        {
            NodeSocket<T> targetSocket;
            
            targetSocket = this.GetEmptySocket();
            portId = targetSocket.ID;

            targetSocket.Link(targetNode, portId, weight);
            return targetSocket.Connection;
            
        }


        public NodeConnection<T> Connect(Node<T> targetNode, int weight = 1) => this.Connect(targetNode, this.GetAutomatedId(), weight);


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
        { return this.GetSocket(ID).Connection; }


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


        #region Private helpers

        // TODO: Might be fun to add my own stack implimentation in the future. 
        // Also note that this is an expensive operation.
        private int GetAutomatedId()
        {
            List<int> storedIds = new List<int>();
            foreach (NodeSocket<T> current in this)
            { storedIds.Add(current.ID); }

            return (storedIds.Count == 0) ? 0 : storedIds.Count + 1;
        }
        #endregion

    }
}
