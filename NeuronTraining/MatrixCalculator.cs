namespace NeuronTraining
{
    public static class MatrixCalculator
    {
        public const double Exp = 2.71828;
        public const double Alpha = 1;

        public static Matrix<double> Square(Matrix<double> matrixToSquare)
        {
            var result = new Matrix<double>(matrixToSquare.RowCount, matrixToSquare.ColumnCount);

            for (int i = 0; i < matrixToSquare.RowCount; i++)
            {
                for (int j = 0; j < matrixToSquare.ColumnCount; j++)
                {
                    result[i, j] = Math.Pow(matrixToSquare[i, j], 2);
                }
            }

            return result;
        }

        public static Matrix<double> Subtract(Matrix<double> minuend, Matrix<double> subtrahend)
        {
            var result = new Matrix<double>(minuend.RowCount, minuend.ColumnCount);

            for (int i = 0; i < minuend.RowCount; i++)
            {
                for (int j = 0; j < minuend.ColumnCount; j++)
                {
                    var diff = minuend[i, j] - subtrahend[i, j];
                    result[i, j] = diff;
                }
            }

            return result;
        }

        public static Matrix<double> Transponse(Matrix<double> matrixToTransponse)
        {
            var result = new Matrix<double>(matrixToTransponse.ColumnCount, matrixToTransponse.RowCount);

            for (int i = 0; i < matrixToTransponse.RowCount; i++)
            {
                for (int j = 0; j < matrixToTransponse.ColumnCount; j++)
                {
                    result[j, i] = matrixToTransponse[i, j];
                }
            }

            return result;
        }

        public static Matrix<double> ApplySigmoid(Matrix<double> inputMatrix)
        {
            var result = new Matrix<double>(inputMatrix.RowCount, inputMatrix.ColumnCount);

            for (int i = 0; i < inputMatrix.RowCount; i++)
            {
                result[i, 0] = Sigmoid(inputMatrix[i, 0]); ;
            }

            return result;
        }

        public static Matrix<double> MultiplyMatrix(Matrix<double> matrixA, Matrix<double> matrixB)
        {
            if (matrixA.ColumnCount != matrixB.RowCount) throw new Exception($"Columns count in A should be equal Row count in B, but was {matrixA.ColumnCount} and {matrixB.RowCount}");

            // Let's start it
            var rows = matrixA.RowCount;
            var cols = matrixB.ColumnCount;
            var result = new Matrix<double>(matrixA.RowCount, matrixB.ColumnCount);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double resSum = 0;
                    var rowA = matrixA.GetRow(i);
                    var colB = matrixB.GetColumn(j);
                    for (int k = 0; k < rowA.Length; k++)
                    {
                        resSum += rowA[k] * colB[k];
                    }

                    result[i, j] = resSum;
                }
            }

            return result;
        }

        public static double Sigmoid(double x)
        {
            var divider = 1 + Math.Pow(Exp, -x);
            var result = 1 / divider;

            return Math.Round(result, 10);
        }
    }
}
