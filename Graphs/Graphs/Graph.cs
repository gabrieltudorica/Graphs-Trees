using System.Collections.Generic;
using System.Linq;
using Graphs.Exceptions;

namespace Graphs
{
    public abstract class Graph
    {
        protected readonly HashSet<Node> Nodes;

        protected Graph()
        {
            Nodes = new HashSet<Node>();
        }

        public void AddNode(Node node)
        {
            if (HasNode(node))
            {
                throw new NodeAlreadyExistsException();
            }

            Nodes.Add(node);
        }        

        public bool HasNode(Node node)
        {
            return Nodes.Contains(node);
        }

        public IEnumerable<Node> GetNodes()
        {
            return Nodes;
        }

        public Node GetNodeByValue(string value)
        {
            foreach (Node node in Nodes.Where(node => node.GetValue() == value))
            {
                return node;
            }

            throw new NodeNotFoundException();
        }

        public int GetCount()
        {
            return Nodes.Count;
        }

        public abstract void RemoveNode(Node node);
        public abstract void AddEdge(Node from, Node to, int cost);
    }
}
