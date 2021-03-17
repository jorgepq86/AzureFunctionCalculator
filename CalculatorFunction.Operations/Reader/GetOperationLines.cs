using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CalculatorFunction.Model;
using CalculatorFunction.Operations.Abstractions;
using CsvHelper;
using CsvHelper.Configuration;

namespace CalculatorFunction.Operations.Reader
{
    public class GetOperationLines : IGetOperationLines
    {
        public IList<OperationLine> GetLines(Stream fileStream)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                HasHeaderRecord = false,
                Delimiter = " "
            };


            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, config);
            var records = new List<OperationLine>();
            var order = 0;
            while (csv.Read())
            {
                var record = new OperationLine
                {
                    OperationName = csv.GetField<string>(0),
                    Value = csv.GetField<int>(1),
                    Order = order
                };
                records.Add(record);
                order++;
            }
            return records;
        }
    }
}