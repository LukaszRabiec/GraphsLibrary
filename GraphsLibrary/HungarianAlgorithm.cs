using System.Collections.Generic;
using GraphsLibrary.HungarianAlgorithmHelpers;

namespace GraphsLibrary
{
    public class HungarianAlgorithm
    {
        private readonly Graph _graph;

        public HungarianAlgorithm(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            _graph = graph;
        }

        public List<Cost> FindMinimumCost()
        {
            var reducer = new Reducer(_graph);
            reducer.ReduceRows();
            var reducedGraph = reducer.ReduceColumns();

            var coverer = new Coverer(reducedGraph);
            var coveredGraph = coverer.CoverRowsAndColumns();

            var costFinder = new CostFinder(coveredGraph, _graph);
            var costs = costFinder.FindZerosToCost();

            return costs;
        }

        
    }
}
