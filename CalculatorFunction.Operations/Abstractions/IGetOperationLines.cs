using System.Collections.Generic;
using System.IO;
using CalculatorFunction.Model;

namespace CalculatorFunction.Operations.Abstractions
{
    public interface IGetOperationLines
    {
        IList<OperationLine> GetLines(Stream fileStream);
    }
}