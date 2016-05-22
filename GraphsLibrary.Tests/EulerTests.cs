using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace GraphsLibrary.Tests
{
    public class EulerTests
    {
        private const string _dirPath = "SampleGraphsData/Euler/";
        private readonly ITestOutputHelper _output;

        public EulerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GraphV1WithEulerCycleShouldReturnRightEulerCycle()
        {
            var euler = new Euler(new Graph(_dirPath + "v1GraphWithCycle.json"));
            var cycle = euler.FindEulerCycle(0);

            cycle.Should().BeEquivalentTo(new Queue<int>(new[] { 0, 1, 2, 3, 4, 2, 0 }));
        }

        [Fact]
        public void GraphV2WithCycleShouldReturnRightEulerCycle()
        {
            var euler = new Euler(new Graph(_dirPath + "v2GraphWithCycle.json"));
            var cycle = euler.FindEulerCycle(0);

            cycle.Should().BeEquivalentTo(new Queue<int>(new[] { 0, 1, 2, 3, 4, 0 }));
        }
    }
}
