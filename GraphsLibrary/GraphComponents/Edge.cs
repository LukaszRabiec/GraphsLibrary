namespace GraphsLibrary.GraphComponents
{
    public class Edge
    {
        public int Vertice1 { get; set; }

        public int Vertice2 { get; set; }

        public int Cost { get; set; }

        public Edge(int vertice1, int vertice2, int cost)
        {
            Vertice1 = vertice1;
            Vertice2 = vertice2;
            Cost = cost;
        }
    }
}
