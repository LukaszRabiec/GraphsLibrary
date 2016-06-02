namespace GraphsLibrary.HungarianAlgorithmHelpers
{
    public enum CoverType
    {
        Row,
        Column
    }

    public class Coverer
    {
        private readonly int[,] _matrixEnlarged;
        private readonly int[,] _matrixCovered;

        public Coverer(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            _matrixEnlarged = CopyAndEnlargeMatrix(graph.AdjacencyMatrixCopy);
            _matrixCovered = new int[graph.AdjacencyMatrix.GetLength(0), graph.AdjacencyMatrix.GetLength(1)];
        }

        private int[,] CopyAndEnlargeMatrix(int[,] matrix)
        {
            var enlargedRows = matrix.GetLength(0) + 1;
            var enlargedCols = matrix.GetLength(1) + 1;

            var resultMatrix = new int[enlargedRows, enlargedCols];

            for (int row = 1; row < enlargedRows; row++)
            {
                for (int col = 1; col < enlargedCols; col++)
                {
                    resultMatrix[row, col] = matrix[row - 1, col - 1];
                }
            }

            return resultMatrix;
        }

        public Graph CoverRowsAndColumns()
        {
            do
            {
                ResetZerosCounter();
                ClearCoveredMatrix();
                CountZerosInRows();
                CountZerosInColumns();
                var coveredZeros = CoverZeros();

                if (coveredZeros == _matrixCovered.GetLength(0))
                {
                    break;
                }

                var minimum = GetMinimumValueFromCovered();
                AddAndSubstractMinValue(minimum);

            } while (true);

            return new Graph(MakeReducedCopyOfEnlarged());
        }

        private void ResetZerosCounter()
        {
            for (int i = 0; i < _matrixEnlarged.GetLength(0); i++)
            {
                _matrixEnlarged[0, i] = 0;
                _matrixEnlarged[i, 0] = 0;
            }
        }

        private void ClearCoveredMatrix()
        {
            for (int row = 0; row < _matrixCovered.GetLength(0); row++)
            {
                for (int col = 0; col < _matrixCovered.GetLength(1); col++)
                {
                    _matrixCovered[row, col] = 0;
                }
            }
        }

        private void CountZerosInRows()
        {
            for (int row = 1; row < _matrixEnlarged.GetLength(0); row++)
            {
                var counter = 0;

                for (int col = 1; col < _matrixEnlarged.GetLength(1); col++)
                {
                    if (_matrixEnlarged[row, col] == 0)
                    {
                        counter++;
                    }
                }

                _matrixEnlarged[row, 0] = counter;
            }
        }

        private void CountZerosInColumns()
        {
            for (int col = 1; col < _matrixEnlarged.GetLength(1); col++)
            {
                var counter = 0;

                for (int row = 1; row < _matrixEnlarged.GetLength(0); row++)
                {
                    if (_matrixEnlarged[row, col] == 0)
                    {
                        counter++;
                    }
                }

                _matrixEnlarged[0, col] = counter;
            }
        }

        private int CoverZeros()
        {
            int coverCounter = 0;

            do
            {
                int maxZerosInRow = GetMaximumZerosCountInRow();
                int maxZerosInCol = GetMaximumZerosCountInCol();

                if (maxZerosInCol > maxZerosInRow)
                {
                    var colIndex = GetIndexofColumnWithMaximumZeros();
                    CoverColumn(colIndex);
                }
                else
                {
                    var rowIndex = GetIndexOfRowWithMaximumZeros();
                    CoverRow(rowIndex);
                }

                coverCounter++;
            } while (AreZerosToCover());

            return coverCounter;
        }

        private int GetMaximumZerosCountInRow()
        {
            int maxInRow = 0;

            for (int row = 1; row < _matrixEnlarged.GetLength(1); row++)
            {
                if (_matrixEnlarged[row, 0] > maxInRow)
                {
                    maxInRow = _matrixEnlarged[row, 0];
                }
            }

            return maxInRow;
        }

        private int GetMaximumZerosCountInCol()
        {
            int maxInCol = 0;

            for (int col = 1; col < _matrixEnlarged.GetLength(1); col++)
            {
                if (_matrixEnlarged[0, col] > maxInCol)
                {
                    maxInCol = _matrixEnlarged[0, col];
                }
            }

            return maxInCol;
        }

        private CoverType GetCoverType(int iteration)
        {
            return iteration % 2 == 0 ? CoverType.Row : CoverType.Column;
        }

        private int GetIndexOfRowWithMaximumZeros()
        {
            int max = int.MinValue;
            int index = 0;
            for (int row = 1; row < _matrixEnlarged.GetLength(0); row++)
            {
                if (_matrixEnlarged[row, 0] > max)
                {
                    max = _matrixEnlarged[row, 0];
                    index = row;
                }
            }

            return index;
        }

        private void CoverRow(int rowIndex)
        {
            const int offset = 1;

            for (int col = 0; col < _matrixCovered.GetLength(1); col++)
            {
                _matrixCovered[rowIndex - offset, col]++;
                
                if (_matrixEnlarged[rowIndex, col + offset] == 0)
                {
                    _matrixEnlarged[0, col + offset]--;
                }
            }
            _matrixEnlarged[rowIndex, 0] = 0;
        }

        private int GetIndexofColumnWithMaximumZeros()
        {
            int max = int.MinValue;
            int index = 0;
            for (int col = 1; col < _matrixEnlarged.GetLength(1); col++)
            {
                if (_matrixEnlarged[0, col] > max)
                {
                    max = _matrixEnlarged[0, col];
                    index = col;
                }
            }

            return index;
        }

        private void CoverColumn(int colIndex)
        {
            const int offset = 1;

            for (int row = 0; row < _matrixCovered.GetLength(1); row++)
            {
                _matrixCovered[row, colIndex - offset]++;

                if (_matrixEnlarged[row + 1, colIndex] == 0)
                {
                    _matrixEnlarged[row + 1, 0]--;
                }
            }
            _matrixEnlarged[0, colIndex] = 0;
        }

        private bool AreZerosToCover()
        {
            for (int i = 1; i < _matrixEnlarged.GetLength(0); i++)
            {
                if (_matrixEnlarged[0, i] > 0 || _matrixEnlarged[i, 0] > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetMinimumValueFromCovered()
        {
            const int offset = 1;
            int min = int.MaxValue;

            for (int row = 0; row < _matrixCovered.GetLength(0); row++)
            {
                for (int col = 0; col < _matrixCovered.GetLength(1); col++)
                {
                    if (_matrixCovered[row, col] == 0)
                    {
                        if (_matrixEnlarged[row + offset, col + offset] < min)
                        {
                            min = _matrixEnlarged[row + offset, col + offset];
                        }
                    }
                }
            }

            return min;
        }

        private void AddAndSubstractMinValue(int minimum)
        {
            const int offset = 1;

            for (int row = 0; row < _matrixCovered.GetLength(0); row++)
            {
                for (int col = 0; col < _matrixCovered.GetLength(1); col++)
                {
                    if (_matrixCovered[row, col] > 1)
                    {
                        _matrixEnlarged[row + offset, col + offset] += minimum;
                    }
                    else if (_matrixCovered[row, col] == 0)
                    {
                        _matrixEnlarged[row + offset, col + offset] -= minimum;
                    }
                }
            }
        }

        private int[,] MakeReducedCopyOfEnlarged()
        {
            const int offset = 1;
            var coveredMatrix = new int[_matrixEnlarged.GetLength(0) - offset, _matrixEnlarged.GetLength(1) - offset];

            for (int row = 0; row < coveredMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < coveredMatrix.GetLength(1); col++)
                {
                    coveredMatrix[row, col] = _matrixEnlarged[row + offset, col + offset];
                }
            }

            return coveredMatrix;
        }
    }
}
