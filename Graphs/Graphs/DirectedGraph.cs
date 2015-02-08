using Graphs.Exceptions;

namespace Graphs
{
    public class DirectedGraph : Graph
    {
        public override void RemoveNode(Node node)
        {
            if (!HasNode(node))
            {
                throw new NodeNotFoundException();
            }

            Nodes.Remove(node);
        }

        public override void AddEdge(Node from, Node to, int cost)
        {
            if (!HasNode(from) || !HasNode(to))
            {
                throw new NodeNotFoundException();
            }

            from.AddNeighbor(to, cost);
        }       
    }
}
