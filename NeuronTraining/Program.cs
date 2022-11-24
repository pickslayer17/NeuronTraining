

namespace NeuronTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Example2();
        }

        public static void Example2()
        {
            var i_nodes = 3;
            var h_nodes = 3;
            var o_nodes = 3;
            var l_rate = 0.2;

            var neuralNetwork = new NeuralNetwork(i_nodes, h_nodes, o_nodes, l_rate);
            var query = neuralNetwork.Query(new double[] { 0.5, 0.2, 0.8 });
            MatrixConsoleWriter.OutMatrix(neuralNetwork.WeightsHiddenOutput);
            MatrixConsoleWriter.OutMatrix(neuralNetwork.WeightsInputHidden);


            //data_file = open("csv/mnist_train.csv", 'r')
            //data_list = data_file.readlines()
            //data_file.close()

            //begin_time = datetime.datetime.now()

            //epochs = 1

            //for n in range(epochs):
            //    for record in data_list:
            //        all_values = record.split(',')
            //        inputs = (numpy.asfarray(all_values[1:]) / 255.0 * 0.99) + 0.01
            //        targets = numpy.zeros(o_nodes) + 0.01
            //        targets[int(all_values[0])] = 0.99
            //        neural_network.train(inputs, targets)
            //        pass
            //    pass

            //data_file = open("csv/mnist_test.csv", 'r')
            //data_list = data_file.readlines()
            //data_file.close()

            //scorecard = []
            //for record in data_list:
            //    all_values = record.split(',')
            //    correct_label = int(all_values[0])
            //    inputs = (numpy.asfarray(all_values[1:]) / 255.0 * 0.99) + 0.01
            //    outputs = neural_network.query(inputs)
            //    label = numpy.argmax(outputs)
            //    print(correct_label, "истинный маркер", label, "ответ сети")
            //    if (label == correct_label):
            //        scorecard.append(1)
            //    else:
            //        scorecard.append(0)
            //    pass
            //print(scorecard)
            //scorecard_array = numpy.asarray(scorecard)
            //print("эффективность = ", scorecard_array.sum() / scorecard_array.size)

            //print(datetime.datetime.now() - begin_time)

            //label = 3
            //targets = numpy.zeros(o_nodes) + 0.01
            //targets[label] = 0.99
            //print("targets", targets)
            //image_data = neural_network.backquery(targets)
            //print(image_data)
            //plt.imshow(image_data.reshape(28, 28), cmap = 'Greys', interpolation = 'nearest')
            //plt.show()
            //pass
        }
    }
}

