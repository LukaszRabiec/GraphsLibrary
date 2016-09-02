using System.Collections.Generic;
using System.Linq;

namespace GraphsLibrary.TravellingSalesmanProblemComponents
{
    public class DepthFirstSearch
    {
        private struct Neighbour
        {
            public int Index { get; }
            public int Cost { get; }

            public Neighbour(int index, int cost)
            {
                Index = index;
                Cost = cost;
            }
        }

        public List<int> PreorderTraversal(int root, int[,] tree)
        {
            var path = new List<int>();
            var stack = new Stack<int>();
            var neighbours = GetNeighboursSortedByWeights(tree);

            stack.Push(root);

            while (stack.Count != 0)
            {
                int current = stack.Pop();
                path.Add(current);

                foreach (var neighbour in neighbours[current]
                    .Where(neighbour => !path.Contains(neighbour.Index))
                    .Select(neighbour => neighbour.Index))
                {
                    stack.Push(neighbour);
                }
            }

            path.Add(root);

            return path;
        }

        private Dictionary<int, List<Neighbour>> GetNeighboursSortedByWeights(int[,] tree)
        {
            var neighbours = new Dictionary<int, List<Neighbour>>();

            for (int vertice = 0; vertice < tree.GetLength(0); vertice++)
            {
                var unsortedNeigbours = new List<Neighbour>();

                for (int neighbour = 0; neighbour < tree.GetLength(1); neighbour++)
                {
                    if (tree[vertice, neighbour] != 0)
                    {
                        unsortedNeigbours.Add(new Neighbour(neighbour, tree[vertice, neighbour]));
                    }
                }

                var sortedNeighbours = unsortedNeigbours.OrderBy(neighbour => neighbour.Cost).ToList();
                neighbours.Add(vertice, sortedNeighbours);
            }

            return neighbours;
        }
    }
}
