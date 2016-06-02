using System;
using FluentAssertions;
using Xunit;

namespace GraphsLibrary.Tests
{
    public class ValidatorTests
    {
        private string _dirPath = "ValidatorGraphsData/";

        [Fact]
        public void NotSquareMatrixShouldThrowArgumentException()
        {
            var notSquareMatrix = new int[2, 3];

            Action validatingNotSquareMatrix = () =>
            {
                Validator.ValidateIfSquareMatrix(notSquareMatrix);
            };

            validatingNotSquareMatrix.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void SquareMatrixShouldNotThrowArgumentException()
        {
            var notSquareMatrix = new int[2, 2];

            Action validatingSquareMatrix = () =>
            {
                Validator.ValidateIfSquareMatrix(notSquareMatrix);
            };

            validatingSquareMatrix.ShouldNotThrow<ArgumentException>();
        }

        [Fact]
        public void UndirectedGraphWhichIsUnsymmetricalShouldThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1UndirectedGraphWhichIsUnsymmetrical.json");

            Action validatingUndirectedGraphWhichIsUnsymmetrical = () =>
            {
                Validator.ValidateUndirectedGraphAdjacency(graph);
            };

            validatingUndirectedGraphWhichIsUnsymmetrical.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void UndirectedGraphWhichIsSymmetricalShouldNotThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1UndirectedGraphWhichIsSymmetrical.json");

            Action validatingUndirectedGraphWhichIsUnsymmetrical = () =>
            {
                Validator.ValidateUndirectedGraphAdjacency(graph);
            };

            validatingUndirectedGraphWhichIsUnsymmetrical.ShouldNotThrow<ArgumentException>();
        }


        [Fact]
        public void GraphWithoutEulerCycleShouldThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphWithoutEulerCycle.json");

            Action validatingGraphWithoutEulerCycle = () =>
            {
                Validator.ValidateIfGraphHasEulerCycle(graph);
            };

            validatingGraphWithoutEulerCycle.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void GraphWithEulerCycleShouldNotThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphWithEulerCycle.json");

            Action validatingGraphWithEulerCycle = () =>
            {
                Validator.ValidateIfGraphHasEulerCycle(graph);
            };

            validatingGraphWithEulerCycle.ShouldNotThrow<ArgumentException>();
        }


        [Fact]
        public void GraphWithoutGivenVerticeShouldThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphWithEulerCycle.json");

            Action validatingGraphWithoutGivenVertice = () =>
            {
                Validator.ValidateIfGraphHasVertice(10, graph.AdjacencyMatrix);
            };

            validatingGraphWithoutGivenVertice.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void GraphWithGivenVerticeShouldNotThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphWithEulerCycle.json");

            Action validatingGraphWithGivenVertice = () =>
            {
                Validator.ValidateIfGraphHasVertice(2, graph.AdjacencyMatrix);
            };

            validatingGraphWithGivenVertice.ShouldNotThrow<ArgumentException>();
        }

        [Fact]
        public void IncorrectlyMatchedGraphShouldThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphMatchedIncorrectly.json");

            Action validatingIncorrectlyMatchedGraph = () =>
            {
                Validator.ValidateIfGraphIsCorrectlyMatched(graph);
            };

            validatingIncorrectlyMatchedGraph.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CorrectlyMatchedGraphShouldNotThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphMatchedCorrectly.json");

            Action validatingCorrectlyMatchedGraph = () =>
            {
                Validator.ValidateIfGraphIsCorrectlyMatched(graph);
            };

            validatingCorrectlyMatchedGraph.ShouldNotThrow<ArgumentException>();
        }

        [Fact]
        public void IncorrectlyDirectedGraphShouldThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphDirectedIncorrectly.json");

            Action validatingIncorrectlyDirectedGraph = () =>
            {
                Validator.ValidateIfGraphIsCorrectlyDirected(graph);
            };

            validatingIncorrectlyDirectedGraph.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CorrectlyDirectedGraphShouldNotThrowArgumentException()
        {
            var graph = new Graph(_dirPath + "v1GraphDirectedCorrectly.json");

            Action validatingCorrectlyDirectedGraph = () =>
            {
                Validator.ValidateIfGraphIsCorrectlyDirected(graph);
            };

            validatingCorrectlyDirectedGraph.ShouldNotThrow<ArgumentException>();
        }

        [Fact]
        public void CheckingIfVerticesAreNotNeighboursShouldReturnFalse()
        {
            var graph = new Graph(_dirPath + "v1GraphWithEulerCycle.json");

            Validator.AreNeighbours(0, 2, graph.AdjacencyMatrix).Should().BeFalse();
        }

        [Fact]
        public void CheckingIfVerticesAreNeighboursShouldReturnTrue()
        {
            var graph = new Graph(_dirPath + "v1GraphWithEulerCycle.json");

            Validator.AreNeighbours(0, 1, graph.AdjacencyMatrix).Should().BeTrue();
        }
    }
}
