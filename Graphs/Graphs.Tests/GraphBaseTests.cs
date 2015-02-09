using System.Collections.Generic;
using System.Linq;
using Graphs.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class GraphBaseTests
    {
        [TestMethod]
        public void When_NodeIsAddedToGraph_GraphNodesCorrectlyReturned()
        {
            var node = new Node("someValue");
            GraphBase graph = GetGraphWith(node);

            Assert.AreEqual(1, graph.GetNodes().ToList().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeAlreadyExistsException))]
        public void When_DuplicatedNodeIsAddedToGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            GraphBase graph = GetGraphWith(node);

            graph.AddNode(node);
        }

        [TestMethod]
        public void When_GraphContainsNode_HasNodeReturnsTrue()
        {
            var node = new Node("someValue");
            GraphBase graph = GetGraphWith(node);

            Assert.IsTrue(graph.HasNode(node));
        }

        [TestMethod]
        public void When_GraphDoesNotCotainNode_HasNodeReturnsFalse()
        {
            var graph = new GraphBase();
            var node = new Node("someValue");

            Assert.IsFalse(graph.HasNode(node));
        }

        [TestMethod]
        public void When_NodesAreAddedToGraph_NodeCountIsIncremented()
        {
            var node = new Node("someValue");
            var node2 = new Node("otherValue");
            GraphBase graph = GetGraphWith(new List<Node> { node, node2 });

            Assert.AreEqual(2, graph.GetCount());
        }

        [TestMethod]
        public void When_NodeExistsInGraph_CorrectNodeIsReturned()
        {
            var node = new Node("someValue");
            var graph = new GraphBase();
            graph.AddNode(node);

            Node expectedNode = graph.GetNodeByValue(node.GetValue());

            Assert.AreEqual(node.GetValue(), expectedNode.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_NodeDoesNotExistInGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var graph = new GraphBase();

            graph.GetNodeByValue(node.GetValue());
        }

        private static GraphBase GetGraphWith(IEnumerable<Node> nodes)
        {
            var graph = new GraphBase();

            foreach (Node node in nodes)
            {
                graph.AddNode(node);
            }

            return graph;
        }

        private static GraphBase GetGraphWith(Node node)
        {
            var graph = new GraphBase();
            graph.AddNode(node);

            return graph;
        }
    }
}
