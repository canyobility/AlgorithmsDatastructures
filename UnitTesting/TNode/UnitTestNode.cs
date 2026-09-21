using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;

using Source.Utility.GenericNode;
using Shouldly;

namespace UnitTesting.TNode
{
    // Expose protected fields for testing
    class TestNode<T> : Node<T>
    { 
        public NodeSocket<T>? TestGetEmptySocket()
        { return this.GetEmptySocket(); }
    }


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

        [Fact]
        public void GetEmptySocket()
        {
            TestNode<string> testNode1 = new TestNode<string>();
            TestNode<string> testNode2 = new TestNode<string>();
            TestNode<string> testNode3 = new TestNode<string>();

            testNode1.AddSocket(); // 0
            testNode1.AddSocket(); // 1
            NodeSocket<string> socket = testNode1.AddSocket(); // 2

            testNode1.Connect(testNode2); // Should use socket 0
            testNode1.Connect(testNode3); // Should use socket 1

            testNode1.TestGetEmptySocket().ShouldBe(socket);
        }

        [Fact]
        public void IndexNodeSockets()
        {
            TestNode<string> testNode1 = new TestNode<string>();
            TestNode<string> testNode2 = new TestNode<string>();

            testNode1.AddSocket(); // 0th port
            testNode1.AddSocket();
            testNode1.AddSocket();

            testNode1.Connect(testNode2);
            testNode1[0].isConnected.ShouldBeTrue();
            testNode1[0].Value!.Target.ShouldBe(testNode2);
        }
    }
}
