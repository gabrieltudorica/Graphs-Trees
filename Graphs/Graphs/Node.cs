using System.Collections.Generic;

namespace Graphs
{
    public class Node
    {
        private readonly string value;
        private readonly List<Node> neighbors;
 
        public Node(string value)
        {
            this.value = value;
            neighbors = new List<Node>();
        }

        public void AddNeighbor(Node neighbor)
        {
            neighbors.Add(neighbor);
        }

        public string GetValue()
        {
            return value;
        }

        public IEnumerable<Node> GetNeighbors()
        {
            return neighbors;
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
            return neighbors.Contains(neighbor);
        }
    }
}
