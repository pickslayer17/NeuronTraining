using System;

namespace NeuronTraining
{
    public class Matrix<T>
    {
        public Type Type { get; }
        private T[][] _matrix;
        public int RowCount { get; }
        public int ColumnCount { get; }
        public T this[int i, int j]
        {
            get { return GetCell(i, j); }
            set { SetCell(i, j, value); }
        }

        public Matrix(int rowsCount, int columnsCount)
        {
            Type = typeof(T);
            _matrix = new T[rowsCount][];
            RowCount = rowsCount;
            ColumnCount = columnsCount;

            for (int i = 0; i < rowsCount; i++)
            {
                _matrix[i] = new T[columnsCount];
            }
        }

        public Matrix(T[,] array) : this(array.GetLength(0), array.GetLength(1))
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    _matrix[i][j] = (T)array.GetValue(i, j);
                }
            }
        }

        public T[] GetRow(int index)
        {
            var result = new T[ColumnCount];

            for (int i = 0; i < ColumnCount; i++)
            {
                result[i] = _matrix[index][i];
            }

            return result;
        }

        public T[] GetColumn(int index)
        {
            var result = new T[RowCount];

            for (int i = 0; i < RowCount; i++)
            {
                result[i] = _matrix[i][index];
            }

            return result;
        }

        private T GetCell(int row, int column)
        {
            return _matrix[row][column];
        }

        private void SetCell(int row, int column, T value)
        {
            _matrix[row][column] = value;
        }
    }
}
