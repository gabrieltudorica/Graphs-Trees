using System.Collections.Generic;
using System.Linq;
using Graphs.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class DirectedGraphTests
    {
        [TestMethod]
        public void When_NodeIsAddedToGraph_GraphNodesCorrectlyReturned()
        {
            var node = new Node("someValue");
            Graph graph = GetGraphWith(node);

            Assert.AreEqual(1, graph.GetNodes().ToList().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeAlreadyExistsException))]
        public void When_DuplicatedNodeIsAddedToGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            Graph graph = GetGraphWith(node);

            graph.AddNode(node);
        }

        [TestMethod]
        public void When_GraphContainsNode_HasNodeReturnsTrue()
        {
            var node = new Node("someValue");
            Graph graph = GetGraphWith(node);

            Assert.IsTrue(graph.HasNode(node));
        }

        [TestMethod]
        public void When_GraphDoesNotCotainNode_HasNodeReturnsFalse()
        {
            var graph = new DirectedGraph();
            var node = new Node("someValue");

            Assert.IsFalse(graph.HasNode(node));
        }

        [TestMethod]
        public void When_NodesAreAddedToGraph_NodeCountIsIncremented()
        {
            var node = new Node("someValue");
            var node2 = new Node("otherValue");
            Graph graph = GetGraphWith(new List<Node>{node, node2});

            Assert.AreEqual(2, graph.GetCount());            
        }

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
        public void When_NodeExistsInGraph_CorrectNodeIsReturned()
        {
            var node = new Node("someValue");
            var graph = new DirectedGraph();
            graph.AddNode(node);

            Node expectedNode = graph.GetNodeByValue(node.GetValue());

            Assert.AreEqual(node.GetValue(), expectedNode.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_NodeDoesNotExistInGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var graph = new DirectedGraph();

            graph.GetNodeByValue(node.GetValue());
        }

        [TestMethod]
        public void When_EdgeIsAdded_ToNodeBecomesNeighborOfFromNode()
        {
            const string fromNodeValue = "someValue";
            var from = new Node(fromNodeValue);
            var to = new Node("someOtherValue");
            var graph = new DirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);

            graph.AddEdge(from, to, 5);

            Node nodeWithNeighbor = graph.GetNodeByValue(fromNodeValue);
            Assert.IsTrue(nodeWithNeighbor.HasNeighbor(to));
            Assert.AreEqual(5, nodeWithNeighbor.GetCostTo(to));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedNeighborException))]
        public void When_EdgeIsDuplicated_ExceptionIsThrown()
        {
            const string fromNodeValue = "someValue";
            var from = new Node(fromNodeValue);
            var to = new Node("someOtherValue");
            var graph = new DirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);
            graph.AddEdge(from, to, 5);

            graph.AddEdge(from, to, 5);

            Node nodeWithNeighbor = graph.GetNodeByValue(fromNodeValue);
            Assert.IsTrue(nodeWithNeighbor.HasNeighbor(to));
            Assert.AreEqual(5, nodeWithNeighbor.GetCostTo(to));
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_InexistentNodeInGraphIsRemoved_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var graph = new DirectedGraph();

            graph.RemoveNode(node);
        }

        [TestMethod]
        public void When_NodeIsRemovedFromGraph_GraphNodesDoNotContainRemovedNode()
        {
            var node = new Node("someValue");
            Graph graph = GetGraphWith(node);

            graph.RemoveNode(node);

            Assert.IsFalse(graph.HasNode(node));
            Assert.AreEqual(0, graph.GetCount());
        }        

        private static Graph GetGraphWith(IEnumerable<Node> nodes)
        {
            var graph = new DirectedGraph();

            foreach (Node node in nodes)
            {
                graph.AddNode(node);
            }

            return graph;
        }

        private static DirectedGraph GetGraphWith(Node node)
        {
            var graph = new DirectedGraph();
            graph.AddNode(node);

            return graph;
        }
    }
}
