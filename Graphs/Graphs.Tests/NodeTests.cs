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
            Node node = GetNodeWith("initialValue");

            Assert.AreEqual("initialValue", node.GetValue());
        }

        [TestMethod]
        public void When_NodeIsInitialized_NeighborCollectionIsEmpty()
        {
            Node node = GetNodeWith("initialValue");

            Assert.AreEqual(0, node.GetNeighbors().ToList().Count);
        }

        [TestMethod]
        public void When_NodeContainsNeighbor_CorrectNeighborCollectionIsReturned()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.AddNeighbor(neighbor, 0);

            Assert.AreEqual(1, node.GetNeighbors().ToList().Count);
            Assert.IsTrue(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NodeContainsNeighbor_NodeRecognizesItAsNeighbor()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.AddNeighbor(neighbor, 0);

            Assert.IsTrue(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NodeDoesNotContainNeighbor_NodeWillNotRecognizeNeighbor()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            Assert.IsFalse(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        [ExpectedException(typeof(NeighborNotFoundException))]
        public void When_NodeDoesNotContainNeighbor_RemovalOfAnUnexistingNeighborThrowsException()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.RemoveNeighbor(neighbor);
        }

        [TestMethod]
        public void When_NodeRemovesNeighbor_NeighborIsRemovedFromNeighborCollection()
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
        public void When_NodeDoesNotContainNeighbor_RetreivingCostToAnInexistingNeighborThrowsException()
        {
            Node node = GetNodeWith("initialValue");
            Node neighbor = GetNodeWith("neighbor");

            node.GetCostTo(neighbor);
        }

        [TestMethod]
        public void When_NodeContainsNeighbor_TheCorrectCostToNeighborIsReturned()
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
