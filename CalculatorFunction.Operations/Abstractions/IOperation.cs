using CalculatorFunction.Model.Enums;

namespace CalculatorFunction.Operations.Abstractions
{
    public interface IOperation
    {
        public OperationType Type { get; }
        
        double ExecuteOperation(double baseNumber, double operatorNumber);
    }
}