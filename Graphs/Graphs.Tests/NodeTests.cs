using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void When_NodeIsInitializedWithAValue_CorrectValueIsReturned()
        {
            const string nodeValue = "initialValue";

            Node node = GetNodeWith(nodeValue);

            Assert.AreEqual(nodeValue, node.GetValue());
        }

        [TestMethod]
        public void When_NodeIsInitialized_NeighborCollectionIsEmpty()
        {
            Node node = GetNodeWith("initialValue");

            Assert.AreEqual(0, node.GetNeighbors().ToList().Count);
        }

        [TestMethod]
        public void When_NeighborIsAddedToNode_CorrectNeighborCollectionIsReturned()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.AddNeighbor(neighbor);

            Assert.AreEqual(1, node.GetNeighbors().ToList().Count);
            Assert.AreEqual(neighbor, node.GetNeighbors().ToList()[0]);
        }

        [TestMethod]
        public void When_NeighborIsAddedToNode_NodeRecognizesItAsNeighbor()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.AddNeighbor(neighbor);

            Assert.IsTrue(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NeighborIsNotAddedToNode_NodeWillNotRecognizeItAsNeighbor()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");
            Node anotherNode = GetNodeWith("anotherNode");

            node.AddNeighbor(neighbor);

            Assert.IsFalse(node.HasNeighbor(anotherNode));
        }

        [TestMethod]
        public void When_NeighborIsNotAddedToNodeAndIsRemoved_NeighborCollectionIsChanged()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");
            Node anotherNeighbor = GetNodeWith("anotherNeighbor");
            node.AddNeighbor(neighbor);
            IEnumerable<Node> expectedNeighbors = node.GetNeighbors();

            node.RemoveNeighbor(anotherNeighbor);

            Assert.AreEqual(expectedNeighbors, node.GetNeighbors());
        }

        [TestMethod]
        public void When_NeighborIsRemoved_NeighborIsRemovedFromNeighborCollection()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");
            Node anotherNeighbor = GetNodeWith("anotherNeighbor");
            node.AddNeighbor(neighbor);
            node.AddNeighbor(anotherNeighbor);

            node.RemoveNeighbor(anotherNeighbor);

            Assert.AreEqual(1, node.GetNeighbors().ToList().Count);
            Assert.IsFalse(node.HasNeighbor(anotherNeighbor));
        }

        private static Node GetNodeWith(string initialValue)
        {
            return new Node(initialValue);
        }
    }
}
