using CalculatorFunction.Model.Enums;
using CalculatorFunction.Operations.Abstractions;

namespace CalculatorFunction.Operations
{
    public class SubtractOperation : IOperation
    {
        public OperationType Type => OperationType.Subtract;

        public double ExecuteOperation(double baseNumber, double operatorNumber) => baseNumber - operatorNumber;
    }
}