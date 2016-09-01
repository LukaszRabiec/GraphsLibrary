using System;
using System.Collections.Generic;
using GraphsLibrary.Utility;
using MMC = GraphsLibrary.MaximalMatchingComponents.MaximalMatchingCommon;

namespace GraphsLibrary.MaximalMatchingComponents
{
    public class MatchingBuilder
    {
        private int[,] _startingAdjacencyMatrixCopy;
        private List<Tuple<int, int>> _path;
        private List<int> _freeVertices;
        private List<Tuple<int, int>> _matchedVertices;
        private List<Tuple<int, int>> _newMatchedVertices;

        public MatchingBuilder(Graph startingGraph, List<Tuple<int, int>> path, List<Tuple<int, int>> matchedVertices)
        {
            Validator.ValidateIfSquareMatrix(startingGraph.AdjacencyMatrix);
            Validator.ValidateUndirectedGraphAdjacency(startingGraph);
            _startingAdjacencyMatrixCopy = startingGraph.AdjacencyMatrixCopy;
            _path = new List<Tuple<int, int>>(path);
            _freeVertices = MMC.CreateFreeVerticesList(startingGraph.AdjacencyMatrix);
            _matchedVertices = new List<Tuple<int, int>>(matchedVertices);
            _newMatchedVertices = new List<Tuple<int, int>>();
        }

        public Graph BuildNewMatching()
        {
            ExtendPathByMatching();

            foreach (var pathElement in _path)
            {
                var vertice = pathElement.Item1;
                var neighbour = pathElement.Item2;
                MMC.CreateMatchedVertices(vertice, neighbour, _newMatchedVertices);
                MMC.IncrementsAdjacencyMatrix(vertice, neighbour, _startingAdjacencyMatrixCopy);
                MMC.RemoveFromFreeVertices(vertice, neighbour, _freeVertices);
            }

            return new Graph(_startingAdjacencyMatrixCopy, _freeVertices, _newMatchedVertices);
        }

        private void ExtendPathByMatching()
        {
            foreach (var matchedVertice in _matchedVertices)
            {
                var isRemoved = false;

                foreach (var pathElement in _path)
                {
                    if (TuplesAreEquals(pathElement, matchedVertice))
                    {
                        _path.Remove(pathElement);
                        isRemoved = true;
                        break;
                    }
                }

                if (!isRemoved)
                {
                    _path.Add(matchedVertice);
                }
            }
                        
        }

        private bool TuplesAreEquals(Tuple<int, int> tuple1, Tuple<int, int> tuple2)
        {
            var firstCondition = tuple1.Equals(tuple2);
            var secondCondition = tuple1.Item1 == tuple2.Item2 && tuple1.Item2 == tuple2.Item1;

            return firstCondition || secondCondition;
        }
    }
}
