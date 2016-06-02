using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphsLibrary.MaximalMatchingBuilders
{
    public class ExtensionPathBuilder
    {
        private int[,] _adjacencyMatrixCopy;
        private List<int> _freeVertices;

        public ExtensionPathBuilder(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            Validator.ValidateIfGraphIsCorrectlyDirected(graph);
            _adjacencyMatrixCopy = graph.AdjacencyMatrixCopy;
            _freeVertices = graph.FreeVertices;
        }

        public Queue<Tuple<int, int>> FindExtensionPath(int startVertice)
        {
            var neighbours = new Stack<int>();
            var extensionPath = new Queue<Tuple<int, int>>();
            neighbours.Push(startVertice);
            var pathFound = false;

            do
            {
                var vertice = neighbours.Pop();

                for (int neighbour = 0; neighbour < _adjacencyMatrixCopy.GetLength(1); neighbour++)
                {
                    if (Validator.AreNeighbours(vertice, neighbour, _adjacencyMatrixCopy))
                    {
                        if (!VerticeIsOnPath(neighbour, extensionPath))
                        {
                            neighbours.Push(neighbour);
                        }
                    }
                }

                if (neighbours.Any())
                {
                    extensionPath.Enqueue(new Tuple<int, int>(vertice, neighbours.Peek()));
                }

                if (_freeVertices.Any() && extensionPath.Any())
                {
                    if (_freeVertices.Contains(extensionPath.Last().Item2))
                    {
                        pathFound = true;
                    }
                }
            } while (!pathFound);

            return extensionPath;
        }

        private bool VerticeIsOnPath(int vertice, Queue<Tuple<int, int>> path)
        {
            var count = path.Count(x => x.Item1 == vertice || x.Item2 == vertice);

            return count > 0;
        }
    }
}
