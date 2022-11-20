namespace NeuronTraining
{
    public static class MatrixCalculator
    {
        public const double Exp = 2.71828;

        public static Matrix Square(Matrix matrixToSquare)
        {
            var result = new Matrix(matrixToSquare.RowCount, matrixToSquare.ColumnCount);

            for (int i = 0; i < matrixToSquare.RowCount; i++)
            {
                for (int j = 0; j < matrixToSquare.ColumnCount; j++)
                {
                    result[i, j] = Math.Pow(matrixToSquare[i, j], 2);
                }
            }

            return result;
        }

        public static Matrix Subtract(Matrix minuend, Matrix subtrahend)
        {
            var result = new Matrix(minuend.RowCount, minuend.ColumnCount);

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

        public static Matrix Transponse(Matrix matrixToTransponse)
        {
            var result = new Matrix(matrixToTransponse.ColumnCount, matrixToTransponse.RowCount);

            for (int i = 0; i < matrixToTransponse.RowCount; i++)
            {
                for (int j = 0; j < matrixToTransponse.ColumnCount; j++)
                {
                    result[j, i] = matrixToTransponse[i, j];
                }
            }

            return result;
        }

        public static Matrix ApplySigmoid(Matrix inputMatrix)
        {
            var result = new Matrix(inputMatrix.RowCount, inputMatrix.ColumnCount);

            for (int i = 0; i < inputMatrix.RowCount; i++)
            {
                var sigmoidRes = Sigmoid(inputMatrix[i, 0]);
                result[i, 0] = sigmoidRes;
            }

            return result;
        }

        public static Matrix GetMatrixFromArray(double[,] array)
        {
            var result = new Matrix(array.GetLength(0), array.GetLength(1));
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[i, j] = (double)array.GetValue(i, j);
                }
            }

            return result;
        }

        public static Matrix MultiplyMatrix(Matrix weightsMatrix, Matrix inputMatrix)
        {
            var result = new Matrix(inputMatrix.RowCount, inputMatrix.ColumnCount);

            for (int i = 0; i < weightsMatrix.RowCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < weightsMatrix.ColumnCount; j++)
                {
                    sum += inputMatrix[j, 0] * weightsMatrix[i, j];
                }

                result[i, 0] = sum;
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
