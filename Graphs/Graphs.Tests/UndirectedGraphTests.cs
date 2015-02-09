using Graphs.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class UndirectedGraphTests
    {
        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_EdgeFromANonExistingNodeInGraphIsAdded_ExceptionIsThrown()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new UndirectedGraph();
            graph.AddNode(to);

            graph.AddEdge(from, to, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_EdgeToANonExistingNodeInGraphIsAdded_ExceptionIsThrown()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new UndirectedGraph();
            graph.AddNode(from);

            graph.AddEdge(from, to, 1);
        }

        [TestMethod]
        public void When_EdgeIsAdded_ToNodeBecomesNeighborOfFromNodeAndViceversa()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new UndirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);

            graph.AddEdge(from, to, 5);

            Assert.IsTrue(from.HasNeighbor(to));
            Assert.AreEqual(5, from.GetCostTo(to));            
            Assert.IsTrue(to.HasNeighbor(from));
            Assert.AreEqual(5, to.GetCostTo(from));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedNeighborException))]
        public void When_EdgeIsDuplicated_ExceptionIsThrown()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new UndirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);
            graph.AddEdge(from, to, 5);

            graph.AddEdge(from, to, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_InexistentNodeInGraphIsRemoved_ExceptionIsThrown()
        {
            var graph = new UndirectedGraph();

            graph.RemoveNode(new Node("someValue"));
        }

        [TestMethod]
        public void When_NodeIsRemovedFromGraph_GraphNodesDoNotContainRemovedNode()
        {
            var node = new Node("someValue");
            var graph = new UndirectedGraph();
            graph.AddNode(node);

            graph.RemoveNode(node);

            Assert.IsFalse(graph.HasNode(node));
            Assert.AreEqual(0, graph.GetCount());
        }

        [TestMethod]
        public void When_NodeIsRemovedFromGraph_AllEdgesToTheRemovedNodeAreCleared()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var to2 = new Node("anotherValue");
            var graph = new UndirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);
            graph.AddNode(to2);
            graph.AddEdge(from, to, 5);
            graph.AddEdge(from, to2, 6);

            graph.RemoveNode(from);

            foreach (Node node in graph.GetNodes())
            {
                Assert.IsFalse(node.HasNeighbor(from));
            }

            Assert.IsFalse(graph.HasNode(from));
        }       
    }
}
