using System.Collections.Generic;
using Graphs.Exceptions;

namespace Graphs
{
    //implemented algorithm found at https://msdn.microsoft.com/en-US/library/ms379574(v=vs.80).aspx
    public class Dijkstra
    {
        private readonly Graph graph;
        private Dictionary<Node, int> costs;
        private Dictionary<Node, Node> routes;

        public Dijkstra(Graph graph)
        {
            if (graph.GetCount() < 2)
            {
                throw new SmallGraphException("Graph must have at least two nodes to calculate minimum path between nodes");
            }

            this.graph = graph;
            InitializeCosts();
            InitializeRoutes();
        }

        public IEnumerable<Node> GetMinimumPathBetween(Node source, Node destination)
        {            
            if (!graph.HasNode(source) || !graph.HasNode(destination))
            {
                throw new NodeNotFoundException();
            }

            FindMinimumPathFrom(source);

            if (routes[destination] == null)
            {
                return new List<Node>();
            }

            return GetPathTo(destination);
        }

        public int GetPathCost(IEnumerable<Node> path)
        {
            Queue<Node> enqueuedNodes = GetEnqueuedNodes(path);
            
            if (enqueuedNodes.Count == 0)
            {
                return 0;
            }

            return SumPathCosts(enqueuedNodes);
        }

        private static Queue<Node> GetEnqueuedNodes(IEnumerable<Node> path)
        {
            var enqueuedNodes = new Queue<Node>();

            foreach (Node node in path)
            {
                enqueuedNodes.Enqueue(node);
            }

            return enqueuedNodes;
        }

        private int SumPathCosts(Queue<Node> nodes)
        {
            int pathCost = 0;

            Node current = nodes.Dequeue();

            while (nodes.Count > 0)
            {
                pathCost += current.GetCostTo(nodes.Peek());
                current = nodes.Dequeue();
            }
            return pathCost;
        }

        private void InitializeCosts()
        {
            costs = new Dictionary<Node, int>();

            foreach (Node node in graph.GetNodes())
            {
                costs.Add(node, int.MaxValue);
            }
        }

        private void InitializeRoutes()
        {
            routes = new Dictionary<Node, Node>();

            foreach (Node node in graph.GetNodes())
            {
                routes.Add(node, null);
            }
        }

        private void FindMinimumPathFrom(Node source)
        {
            costs[source] = 0;

            Queue<Node> nonVisitedNodes = GetNonVisitedNodesStartingWith(source);

            while (nonVisitedNodes.Count > 0)
            {
                FindMinimumCostFor(nonVisitedNodes.Dequeue());
            }
        }

        private Queue<Node> GetNonVisitedNodesStartingWith(Node source)
        {
            var nonVisitedNodes = new Queue<Node>();
            nonVisitedNodes.Enqueue(source);

            foreach (Node node in graph.GetNodes())
            {
                if (!nonVisitedNodes.Contains(node))
                {
                    nonVisitedNodes.Enqueue(node);
                }
            }

            return nonVisitedNodes;
        }

        private void FindMinimumCostFor(Node node)
        {
            foreach (Node neighbor in node.GetNeighbors())
            {
                FindMinimumCostBetween(node, neighbor);
            }
        }

        private void FindMinimumCostBetween(Node node, Node neighbor)
        {
            var cost = GetCostBetween(node, neighbor);

            if (cost >= GetCostFor(neighbor))
            {
                return;
            }

            costs[neighbor] = cost;
            UpdateRoute(node, neighbor);
        }

        private int GetCostBetween(Node node, Node neighbor)
        {
            return GetCostFor(node) + node.GetCostTo(neighbor);
        }

        private int GetCostFor(Node node)
        {
            return costs[node];
        }

        private void UpdateRoute(Node source, Node destination)
        {
            routes[destination] = source;
        }

        private IEnumerable<Node> GetPathTo(Node destination)
        {
            var minimumPath = new List<Node>();

            Node current = destination;

            while (current != null)
            {
                minimumPath.Add(current);
                current = routes[current];
            }

            minimumPath.Reverse();

            return minimumPath;
        }
    }
}