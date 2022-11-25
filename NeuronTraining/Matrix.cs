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

        public Matrix<T> Transponse()
        {
            var result = new Matrix<T>(ColumnCount, RowCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result[j, i] = _matrix[i][j];
                }
            }

            return result;
        }

        public void Fill(T value)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    _matrix[i][j] = value;
                }
            }
        }

        public void FillFunc(Func<T> func)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    _matrix[i][j] = func.Invoke();
                }
            }
        }

        public Matrix<T> Operation(Func<T, T> func)
        {
            var result = new Matrix<T>(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result[i, j] = func.Invoke(_matrix[i][j]);
                }
            }

            return result;
        }

        public static Matrix<T> ConvertArrayToOneDimMatrix(T[] array)
        {
            var result = new Matrix<T>(1, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                result[0, i] = array[i];
            }

            return result;
        }

        public static Matrix<T> operator +(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            if (matrixA.RowCount != matrixB.RowCount || matrixA.ColumnCount != matrixB.ColumnCount) throw new Exception("Matrix should be the same size for this operation");

            var result = new Matrix<T>(matrixA.RowCount, matrixA.ColumnCount);

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    result[i, j] = (dynamic)matrixA[i, j] + (dynamic)matrixB[i, j];
                }
            }

            return result;
        }

        public static Matrix<T> operator *(Matrix<T> matrixA, T value)
        {
            var result = new Matrix<T>(matrixA.RowCount, matrixA.ColumnCount);

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    result[i, j] = (dynamic)matrixA[i, j] * (dynamic)value;
                }
            }

            return result;
        }

        public static Matrix<T> operator *(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            if (matrixA.RowCount != matrixB.RowCount || matrixA.ColumnCount != matrixB.ColumnCount) throw new Exception("Matrix should be the same size for this operation");

            var result = new Matrix<T>(matrixA.RowCount, matrixA.ColumnCount);

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    result[i, j] = (dynamic)matrixA[i, j] * (dynamic)matrixB[i, j];
                }
            }

            return result;
        }

        public static Matrix<T> operator -(Matrix<T> matrixA, Matrix<T> matrixB)
        {
            
            if (matrixA.RowCount != matrixB.RowCount || matrixA.ColumnCount != matrixB.ColumnCount) throw new Exception("Matrix should be the same size for this operation");

            var result = new Matrix<T>(matrixA.RowCount, matrixA.ColumnCount);

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    result[i, j] = (dynamic)matrixA[i, j] - (dynamic)matrixB[i, j];
                }
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
