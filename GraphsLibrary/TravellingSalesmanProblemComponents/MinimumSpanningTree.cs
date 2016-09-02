using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphsLibrary.GraphComponents;
using GraphsLibrary.Utility;

namespace GraphsLibrary.TravellingSalesmanProblemComponents
{
    public class MinimumSpanningTree
    {
        public int[,] TreeMatrix { get; private set; }

        private bool[] _verticeVisited;

        public MinimumSpanningTree(int startedVertice, int numberOfVertices, List<Edge> edges)
        {
            Validator.ValidateIfEdgesHaveUncycledVertices(edges);
            InitializeTree(numberOfVertices);
            CreateTree(startedVertice, numberOfVertices, edges);
        }

        private void InitializeTree(int numberOfVertices)
        {
            TreeMatrix = new int[numberOfVertices, numberOfVertices];
            _verticeVisited = new bool[numberOfVertices];

            for (int index = 0; index < numberOfVertices; index++)
            {
                _verticeVisited[index] = false;
            }
        }

        private void CreateTree(int startedVertice, int numberOfVertices, List<Edge> edges)
        {
            var vertice = startedVertice;
            _verticeVisited[vertice] = true;
            var priorityQueue = new EdgesPriorityQueue();

            // Do for all other vertices (without 'startedVertice') ['i' is iterator, not vertice index]
            for (int i = 1; i < numberOfVertices; i++)
            {
                var incidentEdges = GetIncidentEdges(vertice, edges);
                SetVerticeAsFirst(vertice, incidentEdges);

                AddIncidentEdgesToPriorityQueue(incidentEdges, priorityQueue);

                var edge = DequeuingEdgesWhileVertice2Visited(priorityQueue);

                AddEdgeToTreeMatrix(edge);
                SetVerticeAsVisited(edge.Vertice2);

                vertice = edge.Vertice2;
            }
        }

        private List<Edge> GetIncidentEdges(int vertice, List<Edge> edges)
        {
            return edges.Where(edge => VerticeIsIncidentWithEdge(vertice, edge)).ToList();
        }

        private bool VerticeIsIncidentWithEdge(int vertice, Edge edge)
        {
            return edge.Vertice1 == vertice || edge.Vertice2 == vertice;
        }

        private void SetVerticeAsFirst(int vertice, List<Edge> edges)
        {
            foreach (var edge in edges)
            {
                if (edge.Vertice2 == vertice)
                {
                    edge.Vertice2 = edge.Vertice1;
                    edge.Vertice1 = vertice;
                }
            }
        }

        private void AddIncidentEdgesToPriorityQueue(List<Edge> incidentEdges, EdgesPriorityQueue priorityQueue)
        {
            foreach (var incidentEdge in incidentEdges)
            {
                if (!_verticeVisited[incidentEdge.Vertice2])
                {
                    var edge = new Edge(incidentEdge.Vertice1, incidentEdge.Vertice2, incidentEdge.Cost);
                    priorityQueue.Enqueue(edge);
                }
            }
        }

        private Edge DequeuingEdgesWhileVertice2Visited(EdgesPriorityQueue priorityQueue)
        {
            Edge edge;
            do
            {
                edge = priorityQueue.Dequeue();
            } while (_verticeVisited[edge.Vertice2]);

            return edge;
        }

        private void AddEdgeToTreeMatrix(Edge edge)
        {
            TreeMatrix[edge.Vertice1, edge.Vertice2] = edge.Cost;
            TreeMatrix[edge.Vertice2, edge.Vertice1] = edge.Cost;
        }

        private void SetVerticeAsVisited(int vertice)
        {
            _verticeVisited[vertice] = true;
        }

        public override string ToString()
        {
            return ConvertMatrixToString(TreeMatrix);
        }

        private string ConvertMatrixToString(int[,] matrix)
        {
            string resultString = default(string);
            StringBuilder stringBuilder = new StringBuilder();

            for (int vertice = 0; vertice < matrix.GetLength(0); vertice++)
            {
                stringBuilder.Clear();
                for (int neighbour = 0; neighbour < matrix.GetLength(1); neighbour++)
                {
                    stringBuilder.Append(matrix[vertice, neighbour] + "\t");
                }
                resultString += stringBuilder + "\n";
            }

            return resultString;
        }
    }
}
