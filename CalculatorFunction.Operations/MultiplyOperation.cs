using CalculatorFunction.Model.Enums;
using CalculatorFunction.Operations.Abstractions;

namespace CalculatorFunction.Operations
{
    public class MultiplyOperation : IOperation
    {
        public OperationType Type => OperationType.Multiply;

        public double ExecuteOperation(double baseNumber, double operatorNumber) => baseNumber * operatorNumber;
    }
}