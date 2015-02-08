using System.Collections.Generic;
using System.Linq;
using Graphs.Exceptions;
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

            node.AddNeighbor(neighbor, 0);

            Assert.AreEqual(1, node.GetNeighbors().ToList().Count);
            Assert.IsTrue(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NeighborIsAddedToNode_NodeRecognizesItAsNeighbor()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.AddNeighbor(neighbor, 0);

            Assert.IsTrue(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NeighborIsNotAddedToNode_NodeWillNotRecognizeItAsNeighbor()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            Assert.IsFalse(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NeighborIsNotAddedToNode_RemovalOfAnUnexistingNeighborDoesNotChangeTheNeighborCollection()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");
            IEnumerable<Node> expectedNeighbors = node.GetNeighbors();

            node.RemoveNeighbor(neighbor);

            CollectionAssert.AreEqual(expectedNeighbors.ToList(), node.GetNeighbors().ToList());
        }

        [TestMethod]
        public void When_NeighborIsRemoved_NeighborIsRemovedFromNeighborCollection()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");
            node.AddNeighbor(neighbor, 0);

            node.RemoveNeighbor(neighbor);

            Assert.AreEqual(0, node.GetNeighbors().ToList().Count);
            Assert.IsFalse(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        [ExpectedException(typeof(NeighborNotFoundException))]
        public void When_NeighborIsNotAddedToNode_RetreivingCostToAnInexistingNeighborThrowsException()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.GetCostTo(neighbor);
        }

        [TestMethod]
        public void When_NeighborIsAddedToNode_TheCorrectCostToNeighborIsReturned()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");
            
            node.AddNeighbor(neighbor,5);

            Assert.AreEqual(5, node.GetCostTo(neighbor));
        }

        private static Node GetNodeWith(string initialValue)
        {
            return new Node(initialValue);
        }
    }
}
