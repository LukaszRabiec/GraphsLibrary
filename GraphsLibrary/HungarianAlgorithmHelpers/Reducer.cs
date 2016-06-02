namespace GraphsLibrary.HungarianAlgorithmHelpers
{
    public class Reducer
    {
        private readonly int[,] _matrixCopy;

        public Reducer(Graph graph)
        {
            Validator.ValidateIfSquareMatrix(graph.AdjacencyMatrix);
            _matrixCopy = graph.AdjacencyMatrixCopy;
        }

        public Graph ReduceRows()
        {
            for (int row = 0; row < _matrixCopy.GetLength(0); row++)
            {
                var minRowValue = _matrixCopy[row, 0];
                for (int col = 0; col < _matrixCopy.GetLength(1); col++)
                {
                    if (_matrixCopy[row, col] < minRowValue)
                    {
                        minRowValue = _matrixCopy[row, col];
                    }
                }
                SubstractFromRow(row, minRowValue);
            }

            return new Graph(_matrixCopy);
        }

        private void SubstractFromRow(int row, int value)
        {
            for (int col = 0; col < _matrixCopy.GetLength(1); col++)
            {
                _matrixCopy[row, col] -= value;
            }
        }

        public Graph ReduceColumns()
        {
            for (int col = 0; col < _matrixCopy.GetLength(1); col++)
            {
                var minColValue = _matrixCopy[0, col];
                for (int row = 0; row < _matrixCopy.GetLength(0); row++)
                {
                    if (_matrixCopy[row, col] < minColValue)
                    {
                        minColValue = _matrixCopy[row, col];
                    }
                }
                SubstractFromCol(col, minColValue);
            }

            return new Graph(_matrixCopy);
        }

        private void SubstractFromCol(int col, int value)
        {
            for (int row = 0; row < _matrixCopy.GetLength(0); row++)
            {
                _matrixCopy[row, col] -= value;
            }
        }
    }
}
