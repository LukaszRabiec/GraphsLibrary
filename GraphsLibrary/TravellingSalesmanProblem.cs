using System.Collections.Generic;
using GraphsLibrary.TravellingSalesmanProblemComponents;
using GraphsLibrary.Utility;

namespace GraphsLibrary
{
    public class TravellingSalesmanProblem
    {
        private readonly Graph _graph;

        public TravellingSalesmanProblem(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            Validator.ValidateUndirectedGraphAdjacency(graph);
            Validator.ValidateIfVerticesGraphAreUncycled(graph);

            _graph = graph;
        }

        public List<int> SearchOptimalPath(int startedVertice)
        {
            var numberOfVertices = _graph.AdjacencyMatrix.GetLength(0);
            var edges = _graph.InitializeEdgesInUndirectedGraph(Enums.VerticesType.Uncycle);
            var minimumSpanningTree = new MinimumSpanningTree(startedVertice, numberOfVertices, edges);
            var depthFirstSearch = new DepthFirstSearch();
            var path = depthFirstSearch.PreorderTraversal(startedVertice, minimumSpanningTree.TreeMatrix);

            return path;
        }
    }
}
