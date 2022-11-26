using System.Runtime.CompilerServices;

namespace NeuronTraining
{
    public class CsvReader
    {
        public List<KeyValuePair<int, double[]>> Csv = new List<KeyValuePair<int, double[]>>();
        public double OptimizeValues(double x) => (x / 255 * 0.99) + 0.01;

        public void ReadFile(string fileName)
        {
            var allLines = File.ReadAllLines(fileName);

            foreach (var line in allLines)
            {
                var intLine = line.Split(',');
                var number = int.Parse(intLine[0].ToString());
                var image = intLine.Skip(1).Select(x => double.Parse(x)).ToArray();
                Csv.Add(new KeyValuePair<int, double[]>(number, image));
            }
            OptimizeValues();
            Console.WriteLine("Finished optimize values");
        }

        public void OptimizeValues()
        {
            foreach (var item in Csv)
            {
                var array = item.Value;
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = OptimizeValues(array[i]);
                }
            }
        }
    }
}
