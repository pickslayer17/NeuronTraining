namespace NeuronTraining
{
    public static class MatrixCalculator
    {
        public const double Exp = 2.71828;
        public const double Alpha = 1;

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
                result[i, 0] = Sigmoid(inputMatrix[i, 0]); ;
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

        public static Matrix MultiplyMatrixSquareOnVertical(Matrix weightsMatrix, Matrix inputMatrix)
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

        public static Matrix MultiplyMatrixVerticalOnHorizontal(Matrix vertical, Matrix horizontal)
        {
            var result = new Matrix(vertical.RowCount, horizontal.ColumnCount);

            for (int i = 0; i < vertical.RowCount; i++)
            {
                for (int j = 0; j < horizontal.ColumnCount; j++)
                {
                    result[i, j] = vertical[i, 0] * horizontal[0, j];
                }
            }

            return result;
        }

        public static Matrix DeltaWeights(Matrix error, Matrix sigmoid, Matrix prev)
        {
            prev = Transponse(prev);

            var firstPart = new Matrix(error.RowCount, 1);
            for (int i = 0; i < error.RowCount; i++)
            {
                firstPart[i,0] = error[i, 0] * sigmoid[i, 0] * (1 - sigmoid[i, 0]) * Alpha;
            }

            var result = MultiplyMatrixVerticalOnHorizontal(firstPart, prev);

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
