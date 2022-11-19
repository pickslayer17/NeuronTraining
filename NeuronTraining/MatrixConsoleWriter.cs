namespace NeuronTraining
{
    public static class MatrixConsoleWriter
    {
        public static void OutMatrix(Matrix matrixToOut)
        {
            for (int i = 0; i < matrixToOut.RowCount; i++)
            {
                for (int j = 0; j < matrixToOut.ColumnCount; j++)
                {
                    Console.Write($"[{matrixToOut[i, j]}]");
                }

                Console.WriteLine();
            }
        }
    }
}
