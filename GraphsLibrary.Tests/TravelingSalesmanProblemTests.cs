using FluentAssertions;
using GraphsLibrary.TravelingSalesmanProblemComponents;
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
            graph.InitializeEdgesInUndirectedGraph(Enums.VerticesType.Uncycle);
            var edges = graph.Edges;
            var minimumSpanningTree = new MinimumSpanningTree(0, 6, edges);

            var correctMinimumSpanningTree = new Graph(_dirPathCorrect + "v1GraphTreeMatrix.json");

            minimumSpanningTree.TreeMatrix.ShouldBeEquivalentTo(correctMinimumSpanningTree.AdjacencyMatrix);
            _output.WriteLine(minimumSpanningTree.ToString());
        }

        [Fact]
        public void CreatingMinimumSpanningTreeV2ShouldReturnCorrectTreeMatrix()
        {
            var graph = new Graph(_dirPathSample + "v2Graph.json");
            graph.InitializeEdgesInUndirectedGraph(Enums.VerticesType.Uncycle);
            var edges = graph.Edges;
            var minimumSpanningTree = new MinimumSpanningTree(0, 4, edges);

            var correctMinimumSpanningTree = new Graph(_dirPathCorrect + "v2GraphTreeMatrix.json");

            minimumSpanningTree.TreeMatrix.ShouldBeEquivalentTo(correctMinimumSpanningTree.AdjacencyMatrix);
            _output.WriteLine(minimumSpanningTree.ToString());
        }
    }
}
