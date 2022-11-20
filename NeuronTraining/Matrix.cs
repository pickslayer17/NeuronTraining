namespace NeuronTraining
{
    public class Matrix
    {
        private double[][] _matrix;
        public int RowCount { get; }
        public int ColumnCount { get; }
        public double this[int i, int j]
        {
            get { return GetCell(i, j); }
            set { SetCell(i, j, value); }
        }

        public Matrix(int rowsCount, int columnsCount)
        {
            _matrix = new double[rowsCount][];
            RowCount = rowsCount;
            ColumnCount = columnsCount;

            for (int i = 0; i < rowsCount; i++)
            {
                _matrix[i] = new double[columnsCount];
            }
        }

        private double GetCell(int row, int column)
        {
            return _matrix[row][column];
        }

        private void SetCell(int row, int column, double value)
        {
            _matrix[row][column] = value;
        }
    }
}
