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
            var node = new Node("initialValue");

            Assert.AreEqual("initialValue", node.GetValue());
        }

        [TestMethod]
        public void When_NodeIsInitialized_NeighborCollectionIsEmpty()
        {
            var node = new Node("initialValue");

            Assert.AreEqual(0, node.GetNeighbors().ToList().Count);
        }

        [TestMethod]
        public void When_NodeContainsNeighbor_CorrectNeighborCollectionIsReturned()
        {
            var node = new Node("initialValue");
            var neighbor = new Node("neighbor");

            node.AddNeighbor(neighbor, 0);

            Assert.AreEqual(1, node.GetNeighbors().ToList().Count);
            Assert.IsTrue(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NodeContainsNeighbor_NodeRecognizesItAsNeighbor()
        {
            var node = new Node("initialValue");
            var neighbor = new Node("neighbor");

            node.AddNeighbor(neighbor, 0);

            Assert.IsTrue(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        public void When_NodeDoesNotContainNeighbor_NodeWillNotRecognizeNeighbor()
        {
            var node = new Node("initialValue");
            var neighbor = new Node("neighbor");

            Assert.IsFalse(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        [ExpectedException(typeof(NeighborNotFoundException))]
        public void When_NodeDoesNotContainNeighbor_RemovalOfAnUnexistingNeighborThrowsException()
        {
            var node = new Node("initialValue");
            var neighbor = new Node("neighbor");

            node.RemoveNeighbor(neighbor);
        }

        [TestMethod]
        public void When_NodeRemovesNeighbor_NeighborIsRemovedFromNeighborCollection()
        {
            var node = new Node("initialValue");
            var neighbor = new Node("neighbor");
            node.AddNeighbor(neighbor, 0);

            node.RemoveNeighbor(neighbor);

            Assert.AreEqual(0, node.GetNeighbors().ToList().Count);
            Assert.IsFalse(node.HasNeighbor(neighbor));
        }

        [TestMethod]
        [ExpectedException(typeof(NeighborNotFoundException))]
        public void When_NodeDoesNotContainNeighbor_RetreivingCostToAnInexistingNeighborThrowsException()
        {
            var node = new Node("initialValue");
            var neighbor = new Node("neighbor");

            node.GetCostTo(neighbor);
        }

        [TestMethod]
        public void When_NodeContainsNeighbor_TheCorrectCostToNeighborIsReturned()
        {
            var node = new Node("initialValue");
            var neighbor = new Node("neighbor");
            
            node.AddNeighbor(neighbor,5);

            Assert.AreEqual(5, node.GetCostTo(neighbor));
        }
    }
}
