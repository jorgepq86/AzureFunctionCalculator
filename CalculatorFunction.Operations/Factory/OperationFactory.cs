using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorFunction.Model.Enums;
using CalculatorFunction.Operations.Abstractions;

namespace CalculatorFunction.Operations.Factory
{
    public class OperationFactory : IOperationFactory
    {
        private readonly IEnumerable<IOperation> _availableOperations;

        public OperationFactory(IEnumerable<IOperation> availableOperations)
        {
            _availableOperations = availableOperations;
        }

        public IOperation GetCurrentOperation(string operationName)
        {
            if (!Enum.TryParse(operationName, true, out OperationType currentOperation))
                throw new ArgumentException($"The operation {operationName} is not allowed", nameof(operationName));
            
            var operation = _availableOperations.FirstOrDefault(op => op.Type == currentOperation);
            if (operation == null)
                throw new NotImplementedException($"The operation {operationName} is not implemented");
            return operation;
        }
    }
}