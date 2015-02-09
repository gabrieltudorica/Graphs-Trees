using Graphs.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class DirectedGraphTests
    {      
        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_EdgeFromANonExistingNodeInGraphIsAdded_ExceptionIsThrown()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new DirectedGraph();
            graph.AddNode(to);

            graph.AddEdge(from, to, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_EdgeToANonExistingNodeInGraphIsAdded_ExceptionIsThrown()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new DirectedGraph();
            graph.AddNode(from);

            graph.AddEdge(from, to, 1);
        }

        [TestMethod]
        public void When_EdgeIsAdded_ToNodeBecomesNeighborOfFromNode()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new DirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);

            graph.AddEdge(from, to, 5);

            Assert.IsTrue(from.HasNeighbor(to));
            Assert.AreEqual(5, from.GetCostTo(to));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedNeighborException))]
        public void When_EdgeIsDuplicated_ExceptionIsThrown()
        {
            var from = new Node("someValue");
            var to = new Node("someOtherValue");
            var graph = new DirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);
            graph.AddEdge(from, to, 5);

            graph.AddEdge(from, to, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_InexistentNodeInGraphIsRemoved_ExceptionIsThrown()
        {
            var graph = new DirectedGraph();

            graph.RemoveNode(new Node("someValue"));
        }

        [TestMethod]
        public void When_NodeIsRemovedFromGraph_GraphNodesDoNotContainRemovedNode()
        {
            var node = new Node("someValue");
            var graph = new DirectedGraph();
            graph.AddNode(node);

            graph.RemoveNode(node);

            Assert.IsFalse(graph.HasNode(node));
            Assert.AreEqual(0, graph.GetCount());
        }                
    }
}
