using System;
using System.Collections.Generic;
using System.Text;

using Source.Utility.GenericNode;

namespace UnitTesting.TNode
{
    public class Program
    {
        [Fact]
        public static void CreateGenericNode()
        {
            Node<int> node1 = new Node<int>(5);
            Node<string> node2 = new Node<string>("Test");
            node1.value = 1;
            node2.value = "cat";
        }

        
    }
}
