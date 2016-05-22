using System;

namespace GraphsLibrary
{
    public static class Validator
    {
        public static void ValidateUndirectedGraphAdjacency(Graph graph)
        {
            var matrix = graph.AdjacencyMatrix;

            for (int vertice = 0; vertice < matrix.GetLength(0); vertice++)
            {
                for (int neighbour = 0; neighbour < vertice; neighbour++)
                {
                    if (matrix[vertice, neighbour] != matrix[neighbour, vertice])
                    {
                        throw new ArgumentException("Graph's adjacency matrix isn't symmetrical.");
                    }
                }
            }
        }

        public static void ValidateIfGraphHasEulerCycle(Graph graph)
        {
            var matrix = graph.AdjacencyMatrix;

            for (int vertice = 0; vertice < matrix.GetLength(0); vertice++)
            {
                int neighboursSum = 0;
                for (int neighbour = 0; neighbour < matrix.GetLength(1); neighbour++)
                {
                    neighboursSum += matrix[vertice, neighbour];
                }

                if (neighboursSum % 2 != 0)
                {
                    throw new ArgumentException("Each vertice of the graph should have an even number of adjacent vertices.");
                }
            }
        }

        public static void ValidateIfGraphHasVertice(int vertice, int[,] adjacencyMatrix)
        {
            if (vertice < 0 || vertice >= adjacencyMatrix.GetLength(0))
            {
                throw new ArgumentException("Graph doesn't contains specified vertice.");
            }
        }

        public static void ValidateIfGraphIsCorrectlyMatched(Graph graph)
        {
            var matrix = graph.AdjacencyMatrix;

            for (int vertice = 0; vertice < matrix.GetLength(0); vertice++)
            {
                for (int neighbour = 0; neighbour < vertice; neighbour++)
                {
                    if (matrix[vertice, neighbour] == 2)
                    {
                        if (matrix[neighbour, vertice] != 2)
                        {
                            throw new ArgumentException("Graph isn't correctly matched.");
                        }
                    }
                }
            }
        }

        public static void ValidateIfGraphIsCorrectlyDirected(Graph graph)
        {
            var matrix = graph.AdjacencyMatrix;

            for (int vertice = 0; vertice < matrix.GetLength(0); vertice++)
            {
                for (int neighbour = 0; neighbour < matrix.GetLength(1); neighbour++)
                {
                    if (matrix[vertice, neighbour] == 1)
                    {
                        if (matrix[neighbour, vertice] > 0)
                        {
                            throw new ArgumentException("Graph isn't correctly directed.");
                        }
                    }
                }
            }
        }

        //TODO: Test this
        public static bool AreNeighbours(int vertice, int neighbour, int[,] adjacencyMatrix)
        {
            return adjacencyMatrix[vertice, neighbour] == 1;
        }
    }
}
