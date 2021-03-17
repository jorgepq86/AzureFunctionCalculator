using System.IO;

namespace CalculatorFunction.Model
{
    public class BlobFile
    {
        public string Name { get; set; }
        public Stream Data { get; set; }

        public BlobFile(string name, Stream data)
        {
            Name = name;
            Data = data;
        }
    }
}