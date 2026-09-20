using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTesting.TNode
{
    public class Program
    {
        [Fact]
        public static void CreateGenericNode()
        {
            Node node1 = new Node<int>(5);
            Node node2 = new Node<string>("Test");
            node1.value = 1;
        }
    }
}
