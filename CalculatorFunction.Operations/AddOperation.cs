using CalculatorFunction.Model.Enums;
using CalculatorFunction.Operations.Abstractions;

namespace CalculatorFunction.Operations
{
    public class AddOperation : IOperation
    {
        public OperationType Type => OperationType.Add;

        public double ExecuteOperation(double baseNumber, double operatorNumber) => baseNumber + operatorNumber;
    }
}