namespace NeuronTraining
{
	public class NeuralNetwork
	{
		public const double Exp = 2.71828;

		private int InputNodesCount { get; }
		private int HiddenNodesCount { get; }
		private int OuptutNodesCount { get; }
		private double LearningRate { get; }
		public Matrix WeightsInputHidden;
		public Matrix WeightsHiddenOutput;

		public NeuralNetwork(int iNodesCount, int hNodesCount, int oNodesCount, double lRate)
		{
			InputNodesCount = iNodesCount;
			HiddenNodesCount = hNodesCount;
			OuptutNodesCount = oNodesCount;
			LearningRate = lRate;

			//	self.weights_input_hidden = numpy.random.normal(0.0, pow(self.hidden_nodes, -0.5), (self.hidden_nodes, self.input_nodes))
			double FillFunc() => Math.Pow(200, -0.5) * (new Random().NextDouble() - 0.5);

			WeightsInputHidden = new Matrix(hNodesCount, iNodesCount);
			WeightsInputHidden.FillFunc(() => new Random().NextDouble() - 0.5); //Math.Pow(200, -0.5)
														   //self.weights_hidden_output = numpy.random.normal(0.0, pow(self.output_nodes, -0.5), (self.output_nodes, self.hidden_nodes))
			WeightsHiddenOutput = new Matrix(oNodesCount, hNodesCount);
			WeightsHiddenOutput.FillFunc(() => new Random().NextDouble() - 0.5);

			//self.activation_function = lambda x: scipy.special.expit(x)
			// Sigmoid()
			//self.inverse_activation_function = lambda x: scipy.special.logit(x)
			//The logit function is defined as logit(p) = log(p / (1 - p)).Note that logit(0) = -inf, logit(1) = inf, and logit(p) for p < 0 or p > 1 yields nan.
			// Logit();
		}

		public void Train(double[] inputsList, double[] targestList)
		{
			//	inputs = numpy.array(inputs_list, ndmin = 2).T
			var inputs = Matrix.ConvertArrayToOneLineMatrix(inputsList).Transpose();
			//targets = numpy.array(targets_list, ndmin = 2).T
			var targets = Matrix.ConvertArrayToOneLineMatrix(targestList).Transpose();
			#region TheSameAsInQuery
			// hidden_inputs = numpy.dot(self.weights_input_hidden, inputs)
			var hiddenInputs = MatrixCalculator.MultiplyMatrix(WeightsInputHidden, inputs);
			// hidden_outputs = self.activation_function(hidden_inputs)
			var hiddenOutputs = hiddenInputs.Operation(x => Sigmoid(x));
			// final_inputs = numpy.dot(self.weights_hidden_output, hidden_outputs)
			var finalInputs = MatrixCalculator.MultiplyMatrix(WeightsHiddenOutput, hiddenOutputs);
			// final_outputs = self.activation_function(final_inputs)
			var finalOutputs = finalInputs.Operation(x => Sigmoid(x));
			#endregion
			//# Error output layer
			//	output_errors = targets - final_outputs
			var outputErrors = targets - finalOutputs;
			// # Error hidden layer, распределенные пропроционально весовым коэффициентам связей
			//# и рекомбинированные на скрытых узлах
			//      hidden_errors = numpy.dot(self.weights_hidden_output.T, output_errors)
			var hiddenErrors = MatrixCalculator.MultiplyMatrix(WeightsHiddenOutput.Transpose(), outputErrors);
			//# renew weights for hidden-output layer
			//	self.weights_hidden_output += self.learning_rate * numpy.dot(
			//	(output_errors * final_outputs * (1.0 - final_outputs)), numpy.transpose(hidden_outputs))
			WeightsHiddenOutput += MatrixCalculator.MultiplyMatrix(
				(outputErrors * finalOutputs * (1.0 - finalOutputs)),
				hiddenOutputs.Transpose()) * LearningRate;

			//self.weights_input_hidden += self.learning_rate * numpy.dot(
			//	(hidden_errors * hidden_outputs * (1.0 - hidden_outputs)), numpy.transpose(inputs))
			WeightsInputHidden += MatrixCalculator.MultiplyMatrix(
				hiddenErrors * hiddenOutputs * (1.0 - hiddenOutputs),
				inputs.Transpose()) * LearningRate;
		}

		public double[] Query(double[] inputsList)
		{
			//	inputs = numpy.array(inputs_list, ndmin = 2).T
			var inputs = Matrix.ConvertArrayToOneLineMatrix(inputsList).Transpose();
			// hidden_inputs = numpy.dot(self.weights_input_hidden, inputs)
			var hiddenInputs = MatrixCalculator.MultiplyMatrix(WeightsInputHidden, inputs);
			// hidden_outputs = self.activation_function(hidden_inputs)
			var hiddenOutputs = hiddenInputs.Operation(x => Sigmoid(x));
			// final_inputs = numpy.dot(self.weights_hidden_output, hidden_outputs)
			var finalInputs = MatrixCalculator.MultiplyMatrix(WeightsHiddenOutput, hiddenOutputs);
			// final_outputs = self.activation_function(final_inputs)
			var finalOutputs = finalInputs.Operation(x => Sigmoid(x));

			//return final_outputs
			return finalOutputs.ToArray();
		}

		public double[] BackQuery(double[] targetsList)
		{
			// final_outputs = numpy.array(targets_list, ndmin = 2).T
			var finalOutputs = Matrix.ConvertArrayToOneLineMatrix(targetsList).Transpose();
			// final_inputs = self.inverse_activation_function(final_outputs)
			var finalInputs = finalOutputs.Operation(x => Logit(x));
			// hidden_outputs = numpy.dot(self.weights_hidden_output.T, final_inputs)
			var hiddenOutputs = MatrixCalculator.MultiplyMatrix(WeightsHiddenOutput.Transpose(), finalInputs);

			// # scale them back to 0.01 to .99
			// hidden_outputs -= numpy.min(hidden_outputs)
			hiddenOutputs -= hiddenOutputs.Min();
			//hidden_outputs /= numpy.max(hidden_outputs)
			hiddenOutputs /= hiddenOutputs.Max();
			// hidden_outputs *= 0.98
			hiddenOutputs *= 0.98;
			// hidden_outputs += 0.01
			hiddenOutputs += 0.01;

			// # calculate the signal into the hidden layer
			// hidden_inputs = self.inverse_activation_function(hidden_outputs)
			var hiddenInputs = hiddenOutputs.Operation(x => Logit(x));

			// # calculate the signal out of the input layer
			// inputs = numpy.dot(self.weights_input_hidden.T, hidden_inputs)
			var inputs = MatrixCalculator.MultiplyMatrix(WeightsInputHidden.Transpose(), hiddenInputs);

			// # scale them back to 0.01 to .99
			// inputs -= numpy.min(inputs)
			inputs -= inputs.Min();
			// inputs /= numpy.max(inputs)
			inputs /= inputs.Max();
			// inputs *= 0.98
			inputs *= 0.98;
			// inputs += 0.01
			inputs += 0.01;
			// return inputs

			return inputs.ToArray();
		}

		public double Sigmoid(double x)
		{
			var divider = 1 + Math.Pow(Exp, -x);
			var result = 1 / divider;

			return result;
		}

		public double Logit(double x)
		{
			return Math.Log(x / (1 - x));
		}
	}
}