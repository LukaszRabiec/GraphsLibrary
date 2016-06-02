using System;
using System.Collections.Generic;
using MMC = GraphsLibrary.MaximalMatchingBuilders.MaximalMatchingCommon;

namespace GraphsLibrary.MaximalMatchingBuilders
{
    public class InitialMatchingBuilder
    {
        private int[,] _adjacencyMatrixCopy;
        private List<int> _freeVertices;
        private List<Tuple<int, int>> _matchedVertices;

        public InitialMatchingBuilder(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            Validator.ValidateUndirectedGraphAdjacency(graph);
            _adjacencyMatrixCopy = graph.AdjacencyMatrixCopy;
            _freeVertices = MMC.CreateFreeVerticesList(graph.AdjacencyMatrix);
            _matchedVertices = new List<Tuple<int, int>>();
        }

        public Graph FindInitialMatching()
        {
            for (int vertice = 0; vertice < _adjacencyMatrixCopy.GetLength(0); vertice++)
            {
                for (int neighbour = 0; neighbour < _adjacencyMatrixCopy.GetLength(1); neighbour++)
                {
                    if (Validator.AreNeighbours(vertice, neighbour, _adjacencyMatrixCopy) && AreFreeVertices(vertice, neighbour))
                    {
                        MMC.CreateMatchedVertices(vertice, neighbour, _matchedVertices);
                        MMC.IncrementsAdjacencyMatrix(vertice, neighbour, _adjacencyMatrixCopy);
                        MMC.RemoveFromFreeVertices(vertice, neighbour, _freeVertices);
                        break;
                    }
                }
            }

            return new Graph(_adjacencyMatrixCopy, _freeVertices, _matchedVertices);
        }

        private bool AreFreeVertices(int vertice, int neighbour)
        {
            return _freeVertices.Contains(vertice) && _freeVertices.Contains(neighbour);
        }
    }
}
