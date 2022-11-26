namespace NeuronTraining
{
    public static class MatrixCalculator
    {
        public static Matrix Operation(Matrix matrixA, Matrix matrixB, Func<double, double, double> func)
        {
            if (matrixA.RowCount != matrixB.RowCount || matrixA.ColumnCount != matrixB.RowCount) throw new Exception("Matrix should be the same size for this operation");

            var result = new Matrix(matrixA.RowCount, matrixB.ColumnCount);

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    result[i, j] = func.Invoke(matrixA[i, j], matrixB[i, j]);
                }
            }

            return result;
        }

        public static Matrix MultiplyMatrix(Matrix matrixA, Matrix matrixB)
        {
            if (matrixA.ColumnCount != matrixB.RowCount) throw new Exception($"Columns count in A should be equal Row count in B, but was {matrixA.ColumnCount} and {matrixB.RowCount}");

            // Let's start it
            var rows = matrixA.RowCount;
            var cols = matrixB.ColumnCount;
            var result = new Matrix(matrixA.RowCount, matrixB.ColumnCount);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < matrixA.ColumnCount; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return result;
        }
    }
}
