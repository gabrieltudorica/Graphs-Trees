using System.Linq;
using Graphs.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class GraphBaseTests
    {
        [TestMethod]
        public void When_GraphContainsNode_HasNodeReturnsTrue()
        {
            var node = new Node("someValue");
            var graph = new GraphBase();
            graph.AddNode(node);

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
        public void When_NodeIsAddedToGraph_GraphNodesCorrectlyReturned()
        {
            var node = new Node("someValue");
            var graph = new GraphBase();
            graph.AddNode(node);

            Assert.AreEqual(node, graph.GetNodes().ToList()[0]);
        }

        [TestMethod]
        public void When_NodesAreAddedToGraph_NodeCountIsUpdatedAccordingly()
        {
            var node = new Node("someValue");
            var node2 = new Node("otherValue");
            var graph = new GraphBase();
            graph.AddNode(node);
            graph.AddNode(node2);

            Assert.AreEqual(2, graph.GetCount());
        }

        [TestMethod]
        [ExpectedException(typeof(NodeAlreadyExistsException))]
        public void When_DuplicatedNodeIsAddedToGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var graph = new GraphBase();
            graph.AddNode(node);

            graph.AddNode(node);
        }                      

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_NodeDoesNotExistInGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var graph = new GraphBase();

            graph.GetNodeByValue(node.GetValue());
        }        
    }
}
