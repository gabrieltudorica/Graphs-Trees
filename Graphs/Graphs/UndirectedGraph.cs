using System.Linq;
using Graphs.Exceptions;

namespace Graphs
{
    public class UndirectedGraph : Graph
    {       
        public override void AddEdge(Node from, Node to, int cost)
        {
            if (!HasNode(from) || !HasNode(to))
            {
                throw new NodeNotFoundException();
            }

            from.AddNeighbor(to, cost);
            to.AddNeighbor(from, cost);
        }

        public override void RemoveNode(Node node)
        {
            if (!HasNode(node))
            {
                throw new NodeNotFoundException();
            }

            Nodes.Remove(node);
            RemoveNeighborReferencesOf(node);
        }

        private void RemoveNeighborReferencesOf(Node toDelete)
        {
            foreach (Node node in Nodes.Where(node => node.HasNeighbor(toDelete)))
            {
                node.RemoveNeighbor(toDelete);
            }
        }
    }
}
