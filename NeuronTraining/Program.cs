

using System.Reflection.Emit;
using System.Reflection.PortableExecutable;

namespace NeuronTraining
{
    public class Program
    {
        public const string Test10FileName = "mnist_test_10.csv";
        public const string TestFileName = "mnist_test.csv";
        public const string TrainFileName = "mnist_train.csv";

        public static int i_nodes = 784;
        public static int h_nodes = 100;
        public static int o_nodes = 10;
        public static double l_rate = 0.1;


        public static void Main(string[] args)
        {
            var neuralNetwork = new NeuralNetwork(i_nodes, h_nodes, o_nodes, l_rate);

            TrainNetwork(neuralNetwork, TestFileName, 1);
            CountEfficiency(neuralNetwork, Test10FileName);
        }

        public static void BackQuery(NeuralNetwork neuralNetwork, int label)
        {

            //label = 3
            //targets = numpy.zeros(o_nodes) + 0.01
            //targets[label] = 0.99
            var targets = Enumerable.Range(0, o_nodes).Select(x => 0.01).ToArray();
            targets[label] = 0.99;

            //print("targets", targets)
            //image_data = neural_network.backquery(targets)
            var imageData = neuralNetwork.BackQuery(targets);
            //print(image_data)
            //plt.imshow(image_data.reshape(28, 28), cmap = 'Greys', interpolation = 'nearest')
            //plt.show()
            //pass
        }

        public static void TrainNetwork(NeuralNetwork neuralNetwork, string fileName, int epochs)
        {
            var reader = new CsvReader();
            reader.ReadFile(fileName);

            double[] inputs;
            double[] targetsWithoutTarget = Enumerable.Range(0, o_nodes).Select(x => 0.01).ToArray();

            for (int i = 0; i < epochs; i++)
            {
                foreach (var item in reader.Csv)
                {
                    var number = item.Key;
                    inputs = item.Value;
                    var targets = targetsWithoutTarget;
                    targets[number] = 0.99;
                    neuralNetwork.Train(inputs, targets);
                }
            }

            inputs = reader.Csv[0].Value;
            var result = neuralNetwork.Query(inputs);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine(result[i]);
            }
        }

        public static void CountEfficiency(NeuralNetwork neuralNetwork, string fileName)
        {
            var reader = new CsvReader();
            reader.ReadFile(fileName);
            var scoreCard = new List<int>();
            foreach (var item in reader.Csv)
            {
                var number = item.Key;
                var inputs = item.Value;
                var targets = Enumerable.Range(0, o_nodes).Select(x => 0.01).ToArray();
                targets[number] = 0.99;
                var result2 = neuralNetwork.Query(inputs);
                var answer = Array.IndexOf(result2, result2.Max());
                if (answer == number)
                {
                    scoreCard.Add(1);
                }
                else
                {
                    scoreCard.Add(0);
                }
            }

            double efficienty = (double)scoreCard.Sum() / scoreCard.Count();
            Console.WriteLine($"Efficienty = {efficienty}");
        }
    }
}

