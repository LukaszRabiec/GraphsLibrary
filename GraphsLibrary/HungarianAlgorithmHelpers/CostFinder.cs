using System.Collections.Generic;

namespace GraphsLibrary.HungarianAlgorithmHelpers
{
    public class CostFinder
    {
        private readonly int[,] _matrixOriginal;
        private readonly int[,] _matrixCovered;
        private readonly bool[,] _matrixOfCovers;

        public CostFinder(Graph graphCovered, Graph originalGraph)
        {
            Validator.ValidateIfSquareMatrix(graphCovered.AdjacencyMatrix);
            _matrixCovered = graphCovered.AdjacencyMatrixCopy;
            _matrixOfCovers = new bool[_matrixCovered.GetLength(0), _matrixCovered.GetLength(1)];
            _matrixOriginal = originalGraph.AdjacencyMatrixCopy;
        }

        public List<Cost> FindZerosToCost()
        {
            var costZeros = new List<Cost>();

            do
            {
                int rowIndex = GetIndexOfRowWithMinimumZeros();
                var zero = GetFirstZeroFromRow(rowIndex);
                CoverRowAndColumn(zero);
                costZeros.Add(zero);
            } while (costZeros.Count != _matrixOfCovers.GetLength(0));

            return costZeros;
        }

        private int GetIndexOfRowWithMinimumZeros()
        {
            int min = int.MaxValue;
            int index = 0;

            for (int row = 0; row < _matrixCovered.GetLength(0); row++)
            {
                int zerosCounter = GetZerosCountFromRow(row);

                if (zerosCounter < min && zerosCounter > 0)
                {
                    min = zerosCounter;
                    index = row;
                }
            }

            return index;
        }

        private int GetZerosCountFromRow(int rowIndex)
        {
            int zerosCounter = 0;

            for (int col = 0; col < _matrixCovered.GetLength(1); col++)
            {
                if (_matrixCovered[rowIndex, col] == 0 && !_matrixOfCovers[rowIndex, col])
                {
                    zerosCounter++;
                }
            }

            return zerosCounter;
        }

        private Cost GetFirstZeroFromRow(int rowIndex)
        {
            var cost = new Cost();
            int zeroCounter = GetZerosCountFromRow(rowIndex);

            for (int col = 0; col < _matrixCovered.GetLength(1); col++)
            {
                if (_matrixCovered[rowIndex, col] == 0 && !_matrixOfCovers[rowIndex, col])
                {
                    cost.Row = rowIndex;
                    cost.Column = col;
                    cost.Value = _matrixOriginal[rowIndex, col];
                    if (zeroCounter > 1)
                    {
                        cost.Type = CostType.Ambiguous;
                    }
                    break;
                }
            }

            return cost;
        }

        private void CoverRowAndColumn(Cost cost)
        {
            for (int i = 0; i < _matrixOfCovers.GetLength(0); i++)
            {
                _matrixOfCovers[cost.Row, i] = true;
                _matrixOfCovers[i, cost.Column] = true;
            }
        }
    }
}
