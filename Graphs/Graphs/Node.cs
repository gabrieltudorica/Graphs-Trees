using System.Collections.Generic;
using System.Linq;
using Graphs.Exceptions;

namespace Graphs
{
    public class Node
    {
        private readonly string value;
        private readonly Dictionary<Node, int> neighbors;
 
        public Node(string value)
        {
            this.value = value;
            neighbors = new Dictionary<Node, int>();
        }

        public void AddNeighbor(Node neighbor, int cost)
        {
            neighbors.Add(neighbor, cost);
        }

        public string GetValue()
        {
            return value;
        }

        public IEnumerable<Node> GetNeighbors()
        {
            return neighbors.Select(neighbor => neighbor.Key).ToList();
        }

        public void RemoveNeighbor(Node neighbor)
        {
            if (HasNeighbor(neighbor))
            {
                neighbors.Remove(neighbor);
            }
        }

        public bool HasNeighbor(Node neighbor)
        {
            return neighbors.ContainsKey(neighbor);
        }

        public int GetCostTo(Node neighbor)
        {
            if (!HasNeighbor(neighbor))
            {
                throw new NeighborNotFoundException();
            }

            return neighbors[neighbor];
        }
    }
}
