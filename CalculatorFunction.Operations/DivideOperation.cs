using System;
using CalculatorFunction.Model.Enums;
using CalculatorFunction.Operations.Abstractions;

namespace CalculatorFunction.Operations
{
    public class DivideOperation : IOperation
    {
        public OperationType Type => OperationType.Divide;
        
        public double ExecuteOperation(double baseNumber, double operatorNumber)
        {
            if (operatorNumber == 0)
                throw new ArithmeticException("Cannot be divided to Zero");
            return baseNumber / operatorNumber;
        }
    }
}