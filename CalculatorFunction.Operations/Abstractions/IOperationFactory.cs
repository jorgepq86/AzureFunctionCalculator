namespace CalculatorFunction.Operations.Abstractions
{
    public interface IOperationFactory
    {
        IOperation GetCurrentOperation(string operationName);
    }
}