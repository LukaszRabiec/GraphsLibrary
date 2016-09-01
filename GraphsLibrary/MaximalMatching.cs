using System;
using System.Collections.Generic;
using System.Linq;
using GraphsLibrary.MaximalMatchingComponents;
using GraphsLibrary.Utility;

namespace GraphsLibrary
{
    public class MaximalMatching
    {
        private readonly Graph _graph;
        private List<int> _freeVertices;

        public MaximalMatching(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            _graph = graph;
            _freeVertices = new List<int>();
        }

        public Graph FindMaximalMatchedGraph()
        {
            var initialMatchingBuilder = new InitialMatchingBuilder(_graph);
            var initialMatchedGraph = initialMatchingBuilder.FindInitialMatching();

            var graph = initialMatchedGraph;
            List<Tuple<int, int>> path;

            do
            {
                path = new List<Tuple<int, int>>();
                _freeVertices = new List<int>(graph.FreeVertices);
                var directedGraphBuilder = new DirectedGraphBuilder(graph);
                var directedGraph = directedGraphBuilder.CreateDirectedGraph();

                var extensionPathBuilder = new ExtensionPathBuilder(directedGraph);

                foreach (var freeVertice in _freeVertices)
                {
                    var extensionPath = extensionPathBuilder.FindExtensionPath(freeVertice);
                    if (_freeVertices.Any() && extensionPath.Any())
                    {
                        if (_freeVertices.Contains(extensionPath.Last().Item2))
                        {
                            path = extensionPath.ToList();
                            break;
                        }
                    }
                }

                if (path.Any())
                {
                    var matchingBuilder = new MatchingBuilder(_graph, path, graph.MatchedVertices);
                    graph = matchingBuilder.BuildNewMatching();
                }
            } while (path.Any());


            return graph;
        }
    }
}
