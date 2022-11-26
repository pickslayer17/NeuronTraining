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

        public Matrix(double[,] array) : this(array.GetLength(0), array.GetLength(1))
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    _matrix[i][j] = array[i, j];
                }
            }
        }

        public double Min()
        {
            double result = _matrix[0][0];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (_matrix[i][j] < result)
                    {
                        result = _matrix[i][j];
                    }
                }
            }

            return result;
        }

        public double Max()
        {
            double result = _matrix[0][0];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (_matrix[i][j] > result)
                    {
                        result = _matrix[i][j];
                    }
                }
            }

            return result;
        }

        public Matrix Transpose()
        {
            var result = new Matrix(ColumnCount, RowCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result[j, i] = _matrix[i][j];
                }
            }

            return result;
        }

        public void Fill(double value)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    _matrix[i][j] = value;
                }
            }
        }

        public void FillFunc(Func<double> func)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    _matrix[i][j] = func.Invoke();
                }
            }
        }

        public Matrix Operation(Func<double, double> func)
        {
            var result = new Matrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result[i, j] = func.Invoke(_matrix[i][j]);
                }
            }

            return result;
        }

        public double[] ToArray(bool vertical = true)
        {
            if (RowCount > 1 && ColumnCount > 1) throw new Exception("Only 1 stroke matrix could be turned into array");

            double[] result;

            if (vertical)
            {
                result = new double[RowCount];
                for (int i = 0; i < RowCount; i++)
                {
                    result[i] = _matrix[i][0];
                }
            }
            else
            {
                result = new double[ColumnCount];
                for (int i = 0; i < RowCount; i++)
                {
                    result[i] = _matrix[0][i];
                }
            }

            return result;
        }

        public static Matrix ConvertArrayToOneLineMatrix(double[] array)
        {
            var result = new Matrix(1, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                result[0, i] = array[i];
            }

            return result;
        }

        public static Matrix operator +(Matrix matrixA, Matrix matrixB) => OperatorOnTwoMatrices(matrixA, matrixB, (x, y) => x + y);
        public static Matrix operator -(Matrix matrixA, Matrix matrixB) => OperatorOnTwoMatrices(matrixA, matrixB, (x, y) => x - y);
        public static Matrix operator *(Matrix matrixA, Matrix matrixB) => OperatorOnTwoMatrices(matrixA, matrixB, (x, y) => x * y);

        public static Matrix operator +(Matrix matrixA, double value) => OperatorMatrixAndValue(matrixA, value, (x, y) => x + y);
        public static Matrix operator -(Matrix matrixA, double value) => OperatorMatrixAndValue(matrixA, value, (x, y) => x - y);
        public static Matrix operator -(double value, Matrix matrixA) => OperatorMatrixAndValue(matrixA, value, (x, y) => y - x);
        public static Matrix operator *(Matrix matrixA, double value) => OperatorMatrixAndValue(matrixA, value, (x, y) => x * y);
        public static Matrix operator /(Matrix matrixA, double value) => OperatorMatrixAndValue(matrixA, value, (x, y) => x / y);

        private static Matrix OperatorOnTwoMatrices(Matrix matrixA, Matrix matrixB, Func<double, double, double> func)
        {
            if (matrixA.RowCount != matrixB.RowCount || matrixA.ColumnCount != matrixB.ColumnCount) throw new Exception("Matrix should be the same size for this operation");

            var result = new Matrix(matrixA.RowCount, matrixA.ColumnCount);

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    result[i, j] = func.Invoke(matrixA[i, j], matrixB[i, j]);
                }
            }

            return result;
        }

        private static Matrix OperatorMatrixAndValue(Matrix matrixA, double value, Func<double, double, double> func)
        {
            var result = new Matrix(matrixA.RowCount, matrixA.ColumnCount);

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    result[i, j] = func.Invoke(matrixA[i, j], value);
                }
            }

            return result;
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
