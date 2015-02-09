using System.Collections.Generic;
using System.Linq;
using Graphs.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Tests
{
    [TestClass]
    public class DijkstraTest
    {
        [TestMethod]
        [ExpectedException(typeof(SmallGraphException))]
        public void When_GraphHasUnderTwoNodes_ExceptionIsThrown()
        {
            var graph = new DirectedGraph();

            new Dijkstra(graph);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_SourceNodeDoesNotExistInGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var node2 = new Node("otherValue");
            var graph = new DirectedGraph();
            graph.AddNode(node);
            graph.AddNode(node2);
            var dijkstra = new Dijkstra(graph);

            dijkstra.GetMinimumPathBetween(new Node("notInGraph"), node);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void When_DestinationNodeDoesNotExistInGraph_ExceptionIsThrown()
        {
            var node = new Node("someValue");
            var node2 = new Node("otherValue");
            var graph = new DirectedGraph();
            graph.AddNode(node);
            graph.AddNode(node2);
            var dijkstra = new Dijkstra(graph);

            dijkstra.GetMinimumPathBetween(node, new Node("notInGraph"));
        }

        [TestMethod]
        public void When_DestinationNodeIsIsolated_EmptyPathIsReturned()
        {
            var source = new Node("source");
            var node = new Node("someValue");
            var destination = new Node("destination");
            var graph = new DirectedGraph();
            graph.AddNode(source);
            graph.AddNode(node);
            graph.AddNode(destination);
            graph.AddEdge(source, node, 5);
            var dijkstra = new Dijkstra(graph);

            Assert.AreEqual(0, dijkstra.GetMinimumPathBetween(source, destination).ToList().Count);
        }

        [TestMethod]
        public void When_DestinationNodeIsNeighborWithSourceNode_MinimumPathIsReturned()
        {
            var source = new Node("source");
            var node = new Node("someValue");
            var destination = new Node("destination");
            var graph = new DirectedGraph();
            graph.AddNode(source);
            graph.AddNode(node);
            graph.AddNode(destination);
            graph.AddEdge(source, node, 50);
            graph.AddEdge(source, destination, 5);
            var dijkstra = new Dijkstra(graph);

            var expectedPath = new List<Node>{source, destination};
            List<Node> minimumPath = dijkstra.GetMinimumPathBetween(source, destination).ToList();
            
            CollectionAssert.AreEqual(expectedPath, minimumPath);
        }

        [TestMethod]
        public void When_DestinationNodeIsNotInTheSourceNeighborhood_MinimumPathIsReturned()
        {
            var source = new Node("source");
            var longPath = new Node("longPath");
            var shortPath = new Node("shortPath");
            var destination = new Node("destination");
            var graph = new DirectedGraph();
            graph.AddNode(source);
            graph.AddNode(longPath);
            graph.AddNode(shortPath);
            graph.AddNode(destination);
            graph.AddEdge(source, longPath, 50);
            graph.AddEdge(longPath, destination, 2);
            graph.AddEdge(source, shortPath, 5);
            graph.AddEdge(shortPath, destination, 2);
            var dijkstra = new Dijkstra(graph);

            var expectedPath = new List<Node> { source, shortPath, destination };
            List<Node> minimumPath = dijkstra.GetMinimumPathBetween(source, destination).ToList();

            CollectionAssert.AreEqual(expectedPath, minimumPath);
        }


        [TestMethod]
        public void When_NoPathIsFound_ReturnedCostIsZero()
        {
            var source = new Node("source");
            var node = new Node("someValue");
            var destination = new Node("destination");
            var graph = new DirectedGraph();
            graph.AddNode(source);
            graph.AddNode(node);
            graph.AddNode(destination);
            graph.AddEdge(source, node, 5);
            var dijkstra = new Dijkstra(graph);
            List<Node> minimumPath = dijkstra.GetMinimumPathBetween(source, destination).ToList();

            Assert.AreEqual(0, dijkstra.GetPathCost(minimumPath));
        }

        [TestMethod]
        public void When_MinimumPathIsFound_CorrectCostIsReturned()
        {
            var source = new Node("source");
            var longPath = new Node("longPath");
            var shortPath = new Node("shortPath");
            var destination = new Node("destination");
            var graph = new DirectedGraph();
            graph.AddNode(source);
            graph.AddNode(longPath);
            graph.AddNode(shortPath);
            graph.AddNode(destination);
            graph.AddEdge(source, longPath, 50);
            graph.AddEdge(longPath, destination, 2);
            graph.AddEdge(source, shortPath, 5);
            graph.AddEdge(shortPath, destination, 2);
            var dijkstra = new Dijkstra(graph);
            List<Node> minimumPath = dijkstra.GetMinimumPathBetween(source, destination).ToList();

            Assert.AreEqual(7, dijkstra.GetPathCost(minimumPath));
        }
        
        [TestMethod]        
        public void When_DirectedGraphIsComplex_MinimumPathIsReturned()
        {
            //testing graph example from https://msdn.microsoft.com/en-US/library/ms379574(v=vs.80).aspx           
            var newYork = new Node("New York");
            var chicago = new Node("Chicago");
            var sanFrancisco = new Node("San Francisco");
            var denver = new Node("Denver");
            var losAngeles = new Node("Los Angeles");
            var dallas = new Node("Dallas");
            var miami = new Node("Miami");
            var sanDiego = new Node("San Diego");

            var graph = new DirectedGraph();

            graph.AddNode(newYork);
            graph.AddNode(chicago);
            graph.AddNode(sanFrancisco);
            graph.AddNode(denver);
            graph.AddNode(losAngeles);
            graph.AddNode(dallas);
            graph.AddNode(miami);
            graph.AddNode(sanDiego);

            graph.AddEdge(newYork, chicago, 75);
            graph.AddEdge(newYork, denver, 100);
            graph.AddEdge(newYork, dallas, 125);
            graph.AddEdge(newYork, miami, 90);
            graph.AddEdge(chicago, sanFrancisco, 25);
            graph.AddEdge(chicago, denver, 20);
            graph.AddEdge(sanFrancisco, losAngeles, 45);
            graph.AddEdge(sanDiego, losAngeles, 45);
            graph.AddEdge(dallas, sanDiego, 90);
            graph.AddEdge(dallas, losAngeles, 80);
            graph.AddEdge(miami, dallas, 50);
            graph.AddEdge(denver, losAngeles, 100);

            var dijkstra = new Dijkstra(graph);            
            List<Node> minimumPath = dijkstra.GetMinimumPathBetween(newYork, losAngeles).ToList();
            var expectedPath = new List<Node> { newYork, chicago, sanFrancisco, losAngeles };

            CollectionAssert.AreEqual(expectedPath, minimumPath);
            Assert.AreEqual(145, dijkstra.GetPathCost(minimumPath));
        }

        [TestMethod]
        public void When_UndirectedGraphIsComplex_MinimumPathIsReturned()
        {
            //testing graph example from https://msdn.microsoft.com/en-US/library/ms379574(v=vs.80).aspx           
            var newYork = new Node("New York");
            var chicago = new Node("Chicago");
            var sanFrancisco = new Node("San Francisco");
            var denver = new Node("Denver");
            var losAngeles = new Node("Los Angeles");
            var dallas = new Node("Dallas");
            var miami = new Node("Miami");
            var sanDiego = new Node("San Diego");

            var graph = new UndirectedGraph();

            graph.AddNode(newYork);
            graph.AddNode(chicago);
            graph.AddNode(sanFrancisco);
            graph.AddNode(denver);
            graph.AddNode(losAngeles);
            graph.AddNode(dallas);
            graph.AddNode(miami);
            graph.AddNode(sanDiego);

            graph.AddEdge(newYork, chicago, 75);
            graph.AddEdge(newYork, denver, 100);
            graph.AddEdge(newYork, dallas, 125);
            graph.AddEdge(newYork, miami, 90);
            graph.AddEdge(chicago, sanFrancisco, 25);
            graph.AddEdge(chicago, denver, 20);
            graph.AddEdge(sanFrancisco, losAngeles, 45);
            graph.AddEdge(sanDiego, losAngeles, 45);
            graph.AddEdge(dallas, sanDiego, 90);
            graph.AddEdge(dallas, losAngeles, 80);
            graph.AddEdge(miami, dallas, 50);
            graph.AddEdge(denver, losAngeles, 100);

            var dijkstra = new Dijkstra(graph);
            List<Node> minimumPath = dijkstra.GetMinimumPathBetween(newYork, losAngeles).ToList();
            var expectedPath = new List<Node> { newYork, chicago, sanFrancisco, losAngeles };

            CollectionAssert.AreEqual(expectedPath, minimumPath);
            Assert.AreEqual(145, dijkstra.GetPathCost(minimumPath));
        }
    }
}
