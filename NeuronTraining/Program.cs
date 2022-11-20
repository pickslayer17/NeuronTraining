namespace NeuronTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var I = MatrixCalculator.GetMatrixFromArray(new double[,]
            {
                { 0.9 }, // input on 1st
                { 0.1 }, // input on 2nd
                { 0.8 }  // input on 3rd
            });

            var W = MatrixCalculator.GetMatrixFromArray(new double[,]
            {
                { 0.9, 0.3, 0.4 }, // weights to 1st neuron
                { 0.2, 0.8, 0.2 }, // weights to 2nd neuron
                { 0.1, 0.5, 0.6 }  // weights to 3rd neuron
            });

            var X = MatrixCalculator.MultiplyMatrix(W, I);
            var O = MatrixCalculator.ApplySigmoid(X);

            var W2 = MatrixCalculator.GetMatrixFromArray(new double[,]
            {
                { 0.3, 0.7, 0.5 },
                { 0.6, 0.5, 0.2 },
                { 0.8, 0.1, 0.9 }
            });

            var X2 = MatrixCalculator.MultiplyMatrix(W2, O);
            var O2 = MatrixCalculator.ApplySigmoid(X2);

            //--------------------------
            // Final etalon output
            var T = MatrixCalculator.GetMatrixFromArray(new double[,]
            {
                { 1 },
                { 0 },
                { 0 }
            });

            var Efinal = MatrixCalculator.Subtract(T, O2);
            Efinal = MatrixCalculator.Square(Efinal);
            var W2t = MatrixCalculator.Transponse(W2);
            var E2 = MatrixCalculator.MultiplyMatrix(W2t, Efinal);

            Console.WriteLine("first output");
            MatrixConsoleWriter.OutMatrix(X);
            Console.WriteLine("---------------");
            Console.WriteLine("first output (Sigmoid)");
            MatrixConsoleWriter.OutMatrix(O);
            Console.WriteLine("---------------");
            Console.WriteLine("Final output");
            MatrixConsoleWriter.OutMatrix(X2);
            Console.WriteLine("---------------");
            Console.WriteLine("Final output (Sigmoid)");
            MatrixConsoleWriter.OutMatrix(O2);
            Console.WriteLine("---------------");
            Console.WriteLine("Error final layer");
            MatrixConsoleWriter.OutMatrix(Efinal);
            Console.WriteLine("---------------");
            Console.WriteLine("Transposed W2");
            MatrixConsoleWriter.OutMatrix(W2t);
            Console.WriteLine("---------------");
            Console.WriteLine("Erros E2");
            MatrixConsoleWriter.OutMatrix(E2);
        }
    }
}

