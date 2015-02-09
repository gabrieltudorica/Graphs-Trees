namespace Graphs.Tests
{
    public class GraphBase : Graph
    {
        public override void RemoveNode(Node node)
        {
            throw new System.NotImplementedException();
        }

        public override void AddEdge(Node @from, Node to, int cost)
        {
            throw new System.NotImplementedException();
        }
    }
}
