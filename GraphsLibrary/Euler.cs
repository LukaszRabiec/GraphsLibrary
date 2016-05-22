using System.Collections.Generic;
using System.Linq;

namespace GraphsLibrary
{
    public class Euler
    {
        private readonly int[,] _adjacencyMatrixCopy;

        public Euler(Graph graph)
        {
            Validator.ValidateIfGraphHasEulerCycle(graph);
            _adjacencyMatrixCopy = graph.AdjacencyMatrixCopy;
        }

        public Queue<int> FindEulerCycle(int startingVertice)
        {
            Validator.ValidateIfGraphHasVertice(startingVertice, _adjacencyMatrixCopy);

            var cycle = new Queue<int>();
            var stack = new Stack<int>();

            stack.Push(startingVertice);

            while (stack.Count > 0)
            {
                var vertice = stack.First();
                var adjacentVerticeIndex = GetAdjacentVerticeIndex(vertice, _adjacencyMatrixCopy);

                if (adjacentVerticeIndex < 0)
                {
                    stack.Pop();
                    cycle.Enqueue(vertice);
                }
                else
                {
                    stack.Push(adjacentVerticeIndex);
                    _adjacencyMatrixCopy[vertice, adjacentVerticeIndex]--;
                    _adjacencyMatrixCopy[adjacentVerticeIndex, vertice]--;
                }
            }

            return cycle;
        }

        private int GetAdjacentVerticeIndex(int vertice, int[,] adjacencyMatrix)
        {
            int adjacentVerticeIndex = -1;

            for (int neighbour = 0; neighbour < adjacencyMatrix.GetLength(0); neighbour++)
            {
                if (adjacencyMatrix[vertice, neighbour] == 1)
                {
                    adjacentVerticeIndex = neighbour;
                }
            }

            return adjacentVerticeIndex;
        }
    }
}
