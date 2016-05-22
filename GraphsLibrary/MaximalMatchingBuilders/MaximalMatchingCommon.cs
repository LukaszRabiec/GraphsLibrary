using System;
using System.Collections.Generic;

namespace GraphsLibrary.MaximalMatchingBuilders
{
    public static class MaximalMatchingCommon
    {
        public static void CreateMatchedVertices(int vertice, int neighbour, List<Tuple<int, int>> matchedVertices)
        {
            matchedVertices.Add(new Tuple<int, int>(vertice, neighbour));
        }

        public static List<int> CreateFreeVerticesList(int[,] adjacencyMatrix)
        {
            List<int> freeVertices = new List<int>();

            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                freeVertices.Add(i);
            }

            return freeVertices;
        }

        public static void RemoveFromFreeVertices(int vertice, int neighbour, List<int> freeVertices)
        {
            freeVertices.Remove(vertice);
            freeVertices.Remove(neighbour);
        }

        public static void IncrementsAdjacencyMatrix(int vertice, int neighbour, int[,] adjacencyMatrix)
        {
            if (AreInHigherRange(vertice, neighbour, adjacencyMatrix) && AreInLowerRange(vertice, neighbour, adjacencyMatrix))
            {
                adjacencyMatrix[vertice, neighbour]++;
            }

            if (vertice != neighbour && AreInHigherRange(vertice, neighbour, adjacencyMatrix) && AreInLowerRange(vertice, neighbour, adjacencyMatrix))
            {
                adjacencyMatrix[neighbour, vertice]++;
            }
        }

        private static bool AreInHigherRange(int vertice, int neighbour, int[,] adjacencyMatrix)
        {
            return vertice < adjacencyMatrix.GetLength(0) && neighbour < adjacencyMatrix.GetLength(1);
        }

        private static bool AreInLowerRange(int vertice, int neighbour, int[,] adjacencyMatrix)
        {
            return vertice >= 0 && neighbour >= 0;
        }
    }
}
