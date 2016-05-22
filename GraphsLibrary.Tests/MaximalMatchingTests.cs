using System;
using System.Collections.Generic;
using FluentAssertions;
using GraphsLibrary.MaximalMatchingBuilders;
using Xunit;
using Xunit.Abstractions;

namespace GraphsLibrary.Tests
{
    public class MaximalMatchingTests
    {
        private const string _dirPathSample = "SampleGraphsData/MaximalMatching/";
        private const string _dirPathCorrect = "TestsCorrectGraphsData/MaximalMatching/";
        private readonly ITestOutputHelper _output;

        public MaximalMatchingTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void InitialMatchingShouldReturnCorrectGraph()
        {
            var correctInitialMatchedGraph = new Graph(_dirPathCorrect + "v1GraphInitialMatched.json");
            var initialMatchingBuilder = new InitialMatchingBuilder(new Graph(_dirPathSample + "v1Graph.json"));

            var initialMatchedGraph = initialMatchingBuilder.FindInitialMatching();

            _output.WriteLine(initialMatchedGraph.ToString());
            initialMatchedGraph.AdjacencyMatrix.Should().BeEquivalentTo(correctInitialMatchedGraph.AdjacencyMatrix);
        }

        [Fact]
        public void CreatedDirectedGraphShouldReturnCorrectGraph()
        {
            var correctDirectedGraph = new Graph(_dirPathCorrect + "v1GraphDirected.json");
            var initialMatchedGraph = new Graph(_dirPathCorrect + "v1GraphInitialMatched.json");

            var directedGraphBuilder = new DirectedGraphBuilder(initialMatchedGraph);
            var directedGraph = directedGraphBuilder.CreateDirectedGraph();

            _output.WriteLine(directedGraph.ToString());
            directedGraph.AdjacencyMatrix.Should().BeEquivalentTo(correctDirectedGraph.AdjacencyMatrix);
        }

        [Fact]
        public void FindingSinglePathShouldReturnCorrectPath()
        {
            var correctExtensionPath = new Queue<Tuple<int, int>>(new List<Tuple<int, int>>
            {
                new Tuple<int, int>(2,5),
                new Tuple<int, int>(5,1),
                new Tuple<int, int>(1,6)
            });
            
            var initialMatchedGraph = GetInitialMatchedGraph(new Graph(_dirPathSample + "v1Graph.json"));
            var directedGraph = GetDirectedGraph(initialMatchedGraph);

            var extensionPathBuilder = new ExtensionPathBuilder(directedGraph);
            const int startVertice = 2;
            var extensionPath = extensionPathBuilder.FindExtensionPath(startVertice);

            foreach (var connection in extensionPath)
            {
                _output.WriteLine("({0},{1})", connection.Item1, connection.Item2);
            }
            extensionPath.Should().BeEquivalentTo(correctExtensionPath);
        }

        [Fact]
        public void FindingMaximalMatchingShouldReturnCorrectMatchedGraph()
        {
            var correctMaximalMatchedGraph = new Graph(_dirPathCorrect + "v1GraphMaximalMatched.json");
            var graph = new Graph(_dirPathSample + "v1Graph.json");

            var maximalMatching = new MaximalMatching(graph);
            var maximalMatchedGraph = maximalMatching.FindMaximalMatchedGraph();

            _output.WriteLine(maximalMatchedGraph.ToString());
            maximalMatchedGraph.AdjacencyMatrix.Should().BeEquivalentTo(correctMaximalMatchedGraph.AdjacencyMatrix);
        }

        [Fact]
        public void FindingMaximalMatchingInOtherGraphShouldReturnCorrectMatchedGraph()
        {
            var correctMaximalMatchedGraph = new Graph(_dirPathCorrect + "v2GraphMaximalMatched.json");
            var graph = new Graph(_dirPathSample + "v2Graph.json");

            var maximalMatching = new MaximalMatching(graph);
            var maximalMatchedGraph = maximalMatching.FindMaximalMatchedGraph();

            _output.WriteLine(maximalMatchedGraph.ToString());
            maximalMatchedGraph.AdjacencyMatrix.Should().BeEquivalentTo(correctMaximalMatchedGraph.AdjacencyMatrix);
        }

        private Graph GetInitialMatchedGraph(Graph basicGraph)
        {
            var initialMatchingBuilder = new InitialMatchingBuilder(basicGraph);
            return initialMatchingBuilder.FindInitialMatching();
        }

        private Graph GetDirectedGraph(Graph initialMatchedGraph)
        {
            var directedGraphBuilder = new DirectedGraphBuilder(initialMatchedGraph);
            return directedGraphBuilder.CreateDirectedGraph();
        }
    }
}
