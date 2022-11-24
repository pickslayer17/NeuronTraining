using System.ComponentModel;

namespace NeuronTraining
{
	public class NeuralNetwork
	{
		public const double Exp = 2.71828;
		public const double Alpha = 1;

		private int InputNodesCount { get; }
		private int HiddenNodesCount { get; }
		private int OuptutNodesCount { get; }
		private double LearningRate { get; }
		public Matrix<double> WeightsInputHidden;
		public Matrix<double> WeightsHiddenOutput;

		public NeuralNetwork(int iNodesCount, int hNodesCount, int oNodesCount, double lRate)
		{
			InputNodesCount = iNodesCount;
			HiddenNodesCount = hNodesCount;
			OuptutNodesCount = oNodesCount;
			LearningRate = lRate;
			
			//	self.weights_input_hidden = numpy.random.normal(0.0, pow(self.hidden_nodes, -0.5), (self.hidden_nodes, self.input_nodes))
			WeightsInputHidden = new Matrix<double>(hNodesCount, iNodesCount);
			WeightsInputHidden.FillFunc(() => new Random().NextDouble() - 0.5); //Math.Pow(200, -0.5)
			//self.weights_hidden_output = numpy.random.normal(0.0, pow(self.output_nodes, -0.5), (self.output_nodes, self.hidden_nodes))
			WeightsHiddenOutput = new Matrix<double> (oNodesCount, hNodesCount);
			WeightsHiddenOutput.FillFunc(() => new Random().NextDouble() - 0.5);

			//self.activation_function = lambda x: scipy.special.expit(x)
			// Sigmoid()

			//self.inverse_activation_function = lambda x: scipy.special.logit(x)
			///todo
		}

		public void Train(double[] inputsList, double[] targestList)
		{
		//	inputs = numpy.array(inputs_list, ndmin = 2).T

		//targets = numpy.array(targets_list, ndmin = 2).T

		//# The same as in query
		//	hidden_inputs = numpy.dot(self.weights_input_hidden, inputs)

		//hidden_outputs = self.activation_function(hidden_inputs)

		//final_inputs = numpy.dot(self.weights_hidden_output, hidden_outputs)

		//final_outputs = self.activation_function(final_inputs)

		//# Error output layer
		//	output_errors = targets - final_outputs
  //      # Error hidden layer, распределенные пропроционально весовым коэффициентам связей
  //      # и рекомбинированные на скрытых узлах
  //      hidden_errors = numpy.dot(self.weights_hidden_output.T, output_errors)

		//# renew weights for hidden-output layer
		//	self.weights_hidden_output += self.learning_rate * numpy.dot(
		//	(output_errors * final_outputs * (1.0 - final_outputs)), numpy.transpose(hidden_outputs))

		//self.weights_input_hidden += self.learning_rate * numpy.dot(
		//	(hidden_errors * hidden_outputs * (1.0 - hidden_outputs)), numpy.transpose(inputs))
		}

		public Matrix<double> Query(double[] inputsList)
        {
			//	inputs = numpy.array(inputs_list, ndmin = 2).T
			var inputs = Matrix<double>.ConvertArrayToOneDimMatrix(inputsList).Transponse();
		    // hidden_inputs = numpy.dot(self.weights_input_hidden, inputs)
			var hiddenInputs = MatrixCalculator.MultiplyMatrix(WeightsInputHidden, inputs);
			// hidden_outputs = self.activation_function(hidden_inputs)
			var hiddenOutputs = hiddenInputs.Operation(x => Sigmoid(x));
			// final_inputs = numpy.dot(self.weights_hidden_output, hidden_outputs)
			var finalInputs = MatrixCalculator.MultiplyMatrix(WeightsHiddenOutput, hiddenOutputs);
			// final_outputs = self.activation_function(final_inputs)
			var finalOutputs = finalInputs.Operation(x => Sigmoid(x));

			//return final_outputs
			return finalOutputs;
        }

		public double[] BackQuery(double[] targetsList)
        {
//# transpose the targets list to a vertical array
//			final_outputs = numpy.array(targets_list, ndmin = 2).T

//		# calculate the signal into the final output layer
//			final_inputs = self.inverse_activation_function(final_outputs)

//		# calculate the signal out of the hidden layer
//			hidden_outputs = numpy.dot(self.weights_hidden_output.T, final_inputs)
//        # scale them back to 0.01 to .99
//        hidden_outputs -= numpy.min(hidden_outputs)

//		hidden_outputs /= numpy.max(hidden_outputs)

//		hidden_outputs *= 0.98

//		hidden_outputs += 0.01

//		# calculate the signal into the hidden layer
//			hidden_inputs = self.inverse_activation_function(hidden_outputs)

//		# calculate the signal out of the input layer
//			inputs = numpy.dot(self.weights_input_hidden.T, hidden_inputs)
//        # scale them back to 0.01 to .99
//        inputs -= numpy.min(inputs)

//		inputs /= numpy.max(inputs)

//		inputs *= 0.98

//		inputs += 0.01

//		return inputs
			return null;
        }

		public double Sigmoid(double x)
		{
			var divider = 1 + Math.Pow(Exp, -x);
			var result = 1 / divider;

			return Math.Round(result, 10);
		}
	}
}