using System.Collections.Generic;
using FluentAssertions;
using GraphsLibrary.HungarianAlgorithmComponents;
using GraphsLibrary.Utility;
using Xunit;
using Xunit.Abstractions;

namespace GraphsLibrary.Tests
{
    public class HungarianAlgorithmTests
    {
        private const string _dirPathSample = "SampleGraphsData/Hungarian/";
        private const string _dirPathCorrect = "TestsCorrectGraphsData/Hungarian/";
        private readonly ITestOutputHelper _output;

        public HungarianAlgorithmTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ReducingRowsAndColumnsShouldGiveCorrectReducedGraph()
        {
            var graph = new Graph(_dirPathSample + "v1Graph.json");
            var correctReducedGraph = new Graph(_dirPathCorrect + "v1GraphReduced.json");

            var reducer = new Reducer(graph);
            reducer.ReduceRows();
            var reducedGraph = reducer.ReduceColumns();

            _output.WriteLine(reducedGraph.ToString());
            reducedGraph.AdjacencyMatrix.Should().BeEquivalentTo(correctReducedGraph.AdjacencyMatrix);
        }

        [Fact]
        public void CoveringShouldGiveCorrectCoveredMatrix()
        {
            var graph = new Graph(_dirPathSample + "v1Graph.json");
            var correctCoveredGraph = new Graph(_dirPathCorrect + "v1GraphCovered.json");

            var reducer = new Reducer(graph);
            reducer.ReduceRows();
            var reducedGraph = reducer.ReduceColumns();

            var coverer = new Coverer(reducedGraph);
            var coveredGraph = coverer.CoverRowsAndColumns();

            _output.WriteLine(coveredGraph.ToString());
            coveredGraph.AdjacencyMatrix.ShouldBeEquivalentTo(correctCoveredGraph.AdjacencyMatrix);
        }

        [Fact]
        public void FindingCostsInUnequivocalGraphShouldGiveCorrectUnequivocalCosts()
        {
            var graph = new Graph(_dirPathSample + "v1Graph.json");
            var correctCosts = CreateCorrectUnequivocalCostsForGraphV1();

            var reducer = new Reducer(graph);
            reducer.ReduceRows();
            var reducedGraph = reducer.ReduceColumns();

            var coverer = new Coverer(reducedGraph);
            var coveredGraph = coverer.CoverRowsAndColumns();

            var costFinder = new CostFinder(coveredGraph, graph);
            var costsList = costFinder.FindZerosToCost();

            ShowCosts(costsList);
            CostsListsAreEqual(costsList, correctCosts).Should().BeTrue();
        }

        [Fact]
        public void HungarianAlgorithmWithAmbiguousGraphShouldGiveCostsWithAmbiguousValue()
        {
            var graph = new Graph(_dirPathSample + "v2Graph.json");
            var correctCosts = CreateCorrectAmbiguousCostsForGraphV2();

            var hungarianAlgorithm = new HungarianAlgorithm(graph);
            var costs = hungarianAlgorithm.FindMinimumCost();

            ShowCosts(costs);
            CostsListsAreEqual(costs, correctCosts).Should().BeTrue();
        }

        private List<Cost> CreateCorrectUnequivocalCostsForGraphV1()
        {
            return new List<Cost>
            {
                new Cost
                {
                    Column = 2,
                    Row = 2,
                    Value = 3
                },
                new Cost
                {
                    Column = 0,
                    Row = 3,
                    Value = 2
                },
                new Cost
                {
                    Column = 3,
                    Row = 1,
                    Value = 5
                },
                new Cost
                {
                    Column = 1,
                    Row = 0,
                    Value = 5
                }
            };
        }
        private List<Cost> CreateCorrectAmbiguousCostsForGraphV2()
        {
            return new List<Cost>
            {
                new Cost
                {
                    Column = 1,
                    Row = 1,
                    Value = 1
                },
                new Cost
                {
                    Column = 3,
                    Row = 2,
                    Value = 1
                },
                new Cost
                {
                    Column = 0,
                    Row = 0,
                    Value = 1,
                    Type = Enums.CostType.Ambiguous
                },
                new Cost
                {
                    Column = 2,
                    Row = 3,
                    Value = 3
                }
            };
        }

        private void ShowCosts(List<Cost> costsList)
        {
            foreach (var zero in costsList)
            {
                _output.WriteLine(zero.ToString());
            }
        }

        private bool CostsListsAreEqual(List<Cost> findedCosts, List<Cost> correctCosts)
        {
            int areEquals = int.MinValue;
            
            for (int i = 0; i < findedCosts.Count; i++)
            {
                areEquals = findedCosts[i].CompareTo(correctCosts[i]);
            }

            return areEquals == 0;
        }
    }
}
