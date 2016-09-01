using GraphsLibrary.Utility;

namespace GraphsLibrary.MaximalMatchingComponents
{
    public class DirectedGraphBuilder
    {
        private int[,] _adjacencyMatrixCopy;
        private Graph _graph;

        public DirectedGraphBuilder(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            Validator.ValidateUndirectedGraphAdjacency(graph);
            Validator.ValidateIfGraphIsCorrectlyMatched(graph);
            _adjacencyMatrixCopy = graph.AdjacencyMatrixCopy;
            _graph = graph;
        }

        public Graph CreateDirectedGraph()
        {
            for (int vertice = 0; vertice < _adjacencyMatrixCopy.GetLength(0); vertice++)
            {
                for (int neighbour = 0; neighbour < vertice; neighbour++)
                {
                    if (_adjacencyMatrixCopy[vertice, neighbour] != 0)
                    {
                        if (_adjacencyMatrixCopy[vertice, neighbour] == 2)
                        {
                            _adjacencyMatrixCopy[vertice, neighbour] = 1;
                            _adjacencyMatrixCopy[neighbour, vertice] = 0;
                        }
                        else
                        {
                            _adjacencyMatrixCopy[vertice, neighbour] = 0;
                        }
                    }
                }
            }

            return new Graph(_adjacencyMatrixCopy, _graph.FreeVertices, _graph.MatchedVertices);
        }
    }
}
