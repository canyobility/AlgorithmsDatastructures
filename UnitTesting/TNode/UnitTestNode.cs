using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;

using Source.Utility.GenericNode;
using Shouldly;

namespace UnitTesting.TNode
{
    public class NodeTests
    {
        [Fact]
        public void CreateGenericNode()
        {
            Node<int> node1 = new Node<int>(5);
            Node<string> node2 = new Node<string>();
            node1.value = 1;
            node2.value = "cat";

            
        }

        [Fact]
        public void ConnectNode()
        {
            Node<string> node1 = new Node<string>();
            Node<string> node2 = new Node<string>();

            //NodeSocket<string> socket1 = node1.AddSocket();
            NodeConnection<string> c = node1.Connect(node2, 5);

            node1.GetAtSocket(0).Target.ShouldBe(node2);
        }
    }
}
