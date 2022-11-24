namespace NeuronTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Example1();
        }

        public static void Example2()
        {

        }

        public static void Example1()
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

            var X = MatrixCalculator.MultiplyMatrixSquareOnVertical(W, I);
            var O = MatrixCalculator.ApplySigmoid(X);

            var W2 = MatrixCalculator.GetMatrixFromArray(new double[,]
            {
                { 0.3, 0.7, 0.5 },
                { 0.6, 0.5, 0.2 },
                { 0.8, 0.1, 0.9 }
            });

            var X2 = MatrixCalculator.MultiplyMatrixSquareOnVertical(W2, O);
            var O2 = MatrixCalculator.ApplySigmoid(X2);

            //--------------------------
            // Final etalon output
            var T = MatrixCalculator.GetMatrixFromArray(new double[,]
            {
                { 1 },
                { 0 },
                { 0 }
            });

            // Error
            var Efinal = MatrixCalculator.Subtract(T, O2);

            var DeltaWeightsW2 = MatrixCalculator.DeltaWeights(Efinal, O2, O);
            var newW2 = MatrixCalculator.Subtract(W2, DeltaWeightsW2);


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
            Console.WriteLine("Delta Weights");
            MatrixConsoleWriter.OutMatrix(DeltaWeightsW2);
            Console.WriteLine("---------------");
            Console.WriteLine("New W2");
            MatrixConsoleWriter.OutMatrix(newW2);
        }
    }
}

