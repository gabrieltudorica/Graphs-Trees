using System.Collections.Generic;
using System.Linq;
using Graphs.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class UnUndirectedGraphTests
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
            var graph = new UndirectedGraph();
            var node = new Node("someValue");

            Assert.IsFalse(graph.HasNode(node));
        }

        [TestMethod]
        public void When_NodesAreAddedToGraph_NodeCountIsIncremented()
        {
            var node = new Node("someValue");
            var node2 = new Node("otherValue");
            Graph graph = GetGraphWith(new List<Node> { node, node2 });

            Assert.AreEqual(2, graph.GetCount());
        }

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
        public void When_NodeExistsInGraph_CorrectNodeIsReturned()
        {
            var node = new Node("someValue");
            var graph = new UndirectedGraph();
            graph.AddNode(node);

            Node expectedNode = graph.GetNodeByValue(node.GetValue());

            Assert.AreEqual(node.GetValue(), expectedNode.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_NodeDoesNotExistInGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var graph = new UndirectedGraph();

            graph.GetNodeByValue(node.GetValue());
        }

        [TestMethod]
        public void When_EdgeIsAdded_ToNodeBecomesNeighborOfFromNodeAndViceversa()
        {
            const string fromNodeValue = "someValue";
            const string toNodeValue = "someOtherValue";

            var from = new Node(fromNodeValue);
            var to = new Node(toNodeValue);
            var graph = new UndirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);

            graph.AddEdge(from, to, 5);
            
            Node fromNodeInGraph = graph.GetNodeByValue(fromNodeValue);
            Node toNodeInGraph = graph.GetNodeByValue(toNodeValue);
            Assert.IsTrue(fromNodeInGraph.HasNeighbor(toNodeInGraph));
            Assert.AreEqual(5, fromNodeInGraph.GetCostTo(toNodeInGraph));            
            Assert.IsTrue(toNodeInGraph.HasNeighbor(fromNodeInGraph));
            Assert.AreEqual(5, toNodeInGraph.GetCostTo(fromNodeInGraph));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedNeighborException))]
        public void When_EdgeIsDuplicated_ExceptionIsThrown()
        {
            const string fromNodeValue = "someValue";
            var from = new Node(fromNodeValue);
            var to = new Node("someOtherValue");
            var graph = new UndirectedGraph();
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
            var graph = new UndirectedGraph();

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

        [TestMethod]
        public void When_NodeIsRemovedFromGraph_AllEdgesToTheRemovedNodeAreCleared()
        {
            const string fromNodeValue = "someValue";
            var from = new Node(fromNodeValue);
            var to = new Node("someOtherValue");
            var to2 = new Node("anotherValue");
            var graph = new UndirectedGraph();
            graph.AddNode(from);
            graph.AddNode(to);
            graph.AddNode(to2);
            graph.AddEdge(from, to, 5);
            graph.AddEdge(from, to2, 6);

            graph.RemoveNode(from);

            IEnumerable<Node> graphNodes = graph.GetNodes();
            foreach (Node node in graphNodes)
            {
                Assert.IsFalse(node.HasNeighbor(from));
            }

            Assert.IsFalse(graph.HasNode(from));
        }

        private static Graph GetGraphWith(IEnumerable<Node> nodes)
        {
            var graph = new UndirectedGraph();

            foreach (Node node in nodes)
            {
                graph.AddNode(node);
            }

            return graph;
        }

        private static UndirectedGraph GetGraphWith(Node node)
        {
            var graph = new UndirectedGraph();
            graph.AddNode(node);

            return graph;
        }
    }
}
