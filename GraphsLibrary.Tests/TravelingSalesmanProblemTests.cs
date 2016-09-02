using System.Collections.Generic;
using FluentAssertions;
using GraphsLibrary.TravellingSalesmanProblemComponents;
using GraphsLibrary.Utility;
using Xunit;
using Xunit.Abstractions;

namespace GraphsLibrary.Tests
{
    public class TravelingSalesmanProblemTests
    {
        private const string _dirPathSample = "SampleGraphsData/Salesman/";
        private const string _dirPathCorrect = "TestsCorrectGraphsData/Salesman/";
        private readonly ITestOutputHelper _output;

        public TravelingSalesmanProblemTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CreatingMinimumSpanningTreeV1ShouldReturnCorrectTreeMatrix()
        {
            var graph = new Graph(_dirPathSample + "v1Graph.json");
            var edges = graph.InitializeEdgesInUndirectedGraph(Enums.VerticesType.Uncycle);
            var minimumSpanningTree = new MinimumSpanningTree(0, 6, edges);

            var correctMinimumSpanningTree = new Graph(_dirPathCorrect + "v1GraphTreeMatrix.json");

            minimumSpanningTree.TreeMatrix.ShouldBeEquivalentTo(correctMinimumSpanningTree.AdjacencyMatrix);
            _output.WriteLine(minimumSpanningTree.ToString());
        }

        [Fact]
        public void CreatingMinimumSpanningTreeV2ShouldReturnCorrectTreeMatrix()
        {
            var graph = new Graph(_dirPathSample + "v2Graph.json");
            var edges = graph.InitializeEdgesInUndirectedGraph(Enums.VerticesType.Uncycle);
            var minimumSpanningTree = new MinimumSpanningTree(0, 4, edges);

            var correctMinimumSpanningTree = new Graph(_dirPathCorrect + "v2GraphTreeMatrix.json");

            minimumSpanningTree.TreeMatrix.ShouldBeEquivalentTo(correctMinimumSpanningTree.AdjacencyMatrix);
            _output.WriteLine(minimumSpanningTree.ToString());
        }

        [Fact]
        public void PreorderTraversalShouldReturnCorrectPath()
        {
            var graph = new Graph(_dirPathSample + "v1Graph.json");
            graph.InitializeEdgesInUndirectedGraph(Enums.VerticesType.Uncycle);
            var edges = graph.Edges;
            var startedVertice = 0;
            var minimumSpanningTree = new MinimumSpanningTree(startedVertice, 6, edges);
            var dfs = new DepthFirstSearch();
            var correctPath = new List<int> { 0, 2, 3, 1, 4, 5, 0 };

            var path = dfs.PreorderTraversal(startedVertice, minimumSpanningTree.TreeMatrix);

            path.ShouldBeEquivalentTo(correctPath);
            ShowPath(path);
        }

        [Fact]
        public void SearchingBestPathInGraphV1FromVerticeZeroShouldReturnCorrectPath()
        {
            var graph = new Graph(_dirPathSample + "v1Graph.json");
            var travelingSalesmanProblem = new TravellingSalesmanProblem(graph);
            const int startingVertice = 0;
            var path = travelingSalesmanProblem.SearchOptimalPath(startingVertice);
            var correctPath = new List<int> { 0, 2, 3, 1, 4, 5, 0 };

            path.ShouldBeEquivalentTo(correctPath);
            ShowPath(path);
        }

        [Fact]
        public void SearchingBestPathInGraphV1FromVerticeThreeShouldReturnCorrectPath()
        {
            var graph = new Graph(_dirPathSample + "v1Graph.json");
            var travelingSalesmanProblem = new TravellingSalesmanProblem(graph);
            const int startingVertice = 3;
            var path = travelingSalesmanProblem.SearchOptimalPath(startingVertice);
            var correctPath = new List<int> { 3, 1, 4, 5, 0, 2, 3 };

            path.ShouldBeEquivalentTo(correctPath);
            ShowPath(path);
        }

        [Fact]
        public void SearchingBestPathInGraphV2FromVerticeZeroShouldReturnCorrectPath()
        {
            var graph = new Graph(_dirPathSample + "v2Graph.json");
            var travelingSalesmanProblem = new TravellingSalesmanProblem(graph);
            var path = travelingSalesmanProblem.SearchOptimalPath(0);
            var correctPath = new List<int> { 0, 3, 2, 1, 0 };

            path.ShouldBeEquivalentTo(correctPath);
            ShowPath(path);
        }

        private void ShowPath(List<int> path)
        {
            foreach (var index in path)
            {
                _output.WriteLine(index.ToString());
            }
        }
    }
}
