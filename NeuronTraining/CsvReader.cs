namespace NeuronTraining
{
    public class CsvReader
    {
        public List<KeyValuePair<int, byte[]>> Csv = new List<KeyValuePair<int, byte[]>>();

        public void ReadFile(string fileName)
        {
            var allLines = File.ReadAllLines(fileName);

            foreach (var line in allLines)
            {
                var intLine = line.Split(',');
                int number = int.Parse(intLine[0].ToString());
                byte[] image = intLine.Skip(1).Select(x => byte.Parse(x)).ToArray();
                Csv.Add(new KeyValuePair<int, byte[]>(number, image));
            }
        }
    }
}
