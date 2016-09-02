using System;
using System.Collections.Generic;
using System.Linq;
using GraphsLibrary.GraphComponents;

namespace GraphsLibrary.Utility
{
    public static class Validator
    {
        public static void ValidateIfSquareMatrix(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("Graph's matrix isn't square.");
            }
        }

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

        public static bool AreNeighbours(int vertice, int neighbour, int[,] adjacencyMatrix)
        {
            return adjacencyMatrix[vertice, neighbour] == 1;
        }

        public static void ValidateIfEdgesHaveUncycledVertices(List<Edge> edges)
        {
            if (edges.Any(edge => edge.Vertice1 == edge.Vertice2))
            {
                throw new ArgumentException("You must specified edges with uncycled vertices.");
            }
        }
    }
}
