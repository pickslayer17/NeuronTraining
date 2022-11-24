

namespace NeuronTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Example1();
        }



        public static void Example1()
        {
            var I = new Matrix<double>(new double[,]
            {
                { 0.9 }, // input on 1st
                { 0.1 }, // input on 2nd
                { 0.8 }  // input on 3rd
            });
            MatrixConsoleWriter.OutMatrix(I);

            var W = new Matrix<double>(new double[,]
            {
                { 0.9, 0.3, 0.4 }, // weights to 1st neuron
                { 0.2, 0.8, 0.2 }, // weights to 2nd neuron
                { 0.1, 0.5, 0.6 }  // weights to 3rd neuron
            });
            MatrixConsoleWriter.OutMatrix(W);

            var X = MatrixCalculator.MultiplyMatrix(W, I);
            var O = MatrixCalculator.ApplySigmoid(X);

            var W2 = new Matrix<double>(new double[,]
            {
                { 0.3, 0.7, 0.5 },
                { 0.6, 0.5, 0.2 },
                { 0.8, 0.1, 0.9 }
            });
            MatrixConsoleWriter.OutMatrix(W2);

            var X2 = MatrixCalculator.MultiplyMatrix(W2, O);
            var O2 = MatrixCalculator.ApplySigmoid(X2);

            //--------------------------
            // Final etalon output
            var T = new Matrix<double>(new double[,]
            {
                { 1 },
                { 0 },
                { 0 }
            });



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

        }
    }
}

