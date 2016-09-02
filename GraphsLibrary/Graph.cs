using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphsLibrary.GraphComponents;
using GraphsLibrary.Utility;
using Newtonsoft.Json;

namespace GraphsLibrary
{
    public class Graph
    {
        public int[,] AdjacencyMatrix { get; }

        public int[,] AdjacencyMatrixCopy => DeepCopyOfMatrix(AdjacencyMatrix);

        public List<int> FreeVertices { get; }

        public List<Tuple<int, int>> MatchedVertices { get; }

        public List<Edge> Edges { get; }

        public Graph(string jsonFilePath)
        {
            AdjacencyMatrix = GetGraphFromJsonFile(jsonFilePath);
            FreeVertices = new List<int>();
            MatchedVertices = new List<Tuple<int, int>>();
            Edges = new List<Edge>();
        }
        public Graph(int[,] adjacencyMatrix)
        {
            AdjacencyMatrix = adjacencyMatrix;
            FreeVertices = new List<int>();
            MatchedVertices = new List<Tuple<int, int>>();
            Edges = new List<Edge>();
        }

        public Graph(int[,] adjacencyMatrix, List<int> freeVertices, List<Tuple<int,int>> matchedVertices)
        {
            AdjacencyMatrix = adjacencyMatrix;
            FreeVertices = new List<int>(freeVertices);
            MatchedVertices = new List<Tuple<int, int>>(matchedVertices);
            Edges = new List<Edge>();
        }

        public List<Edge> InitializeEdgesInUndirectedGraph(Enums.VerticesType verticesType)
        {
            var neighbourStartedId = verticesType == Enums.VerticesType.Cycle ? 0 : 1;

            for (int vertice = 0; vertice < AdjacencyMatrix.GetLength(0); vertice++)
            {
                for (int neighbour = neighbourStartedId; neighbour < AdjacencyMatrix.GetLength(1); neighbour++)
                {
                    Edges.Add(new Edge(vertice, neighbour, AdjacencyMatrix[vertice, neighbour]));
                }

                neighbourStartedId++;
            }

            return Edges;
        }

        private int[,] DeepCopyOfMatrix(int[,] sourceMatrix)
        {
            var targetMatrix = new int[sourceMatrix.GetLength(0),sourceMatrix.GetLength(1)];

            for (int vertice = 0; vertice < sourceMatrix.GetLength(0); vertice++)
            {
                for (int neighbour = 0; neighbour < sourceMatrix.GetLength(1); neighbour++)
                {
                    targetMatrix[vertice, neighbour] = sourceMatrix[vertice, neighbour];
                }
            }

            return targetMatrix;
        }

        private int[,] GetGraphFromJsonFile(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<int[,]>(streamReader.ReadToEnd());
            }
        }

        public override string ToString()
        {
            return ConvertMatrixToString(AdjacencyMatrix);
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
