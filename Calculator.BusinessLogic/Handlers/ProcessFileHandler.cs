using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalculatorFunction.Model;
using CalculatorFunction.Model.Enums;
using CalculatorFunction.Model.Request;
using CalculatorFunction.Model.Response;
using CalculatorFunction.Operations.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calculator.BusinessLogic.Handlers
{
    public class ProcessFileHandler : IRequestHandler<ProcessFileRequest, ProcessFileResponse>
    {
        private readonly IGetOperationLines _getOperationLines;
        private readonly IOperationFactory _operationFactory;
        private readonly ILogger _logger;
        
        public ProcessFileHandler(IOperationFactory operationFactory, ILogger<ProcessFileHandler> logger, IGetOperationLines getOperationLines)
        {
            _getOperationLines = getOperationLines;
            _operationFactory = operationFactory;
            _logger = logger;
        }
        
        public Task<ProcessFileResponse> Handle(ProcessFileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidateFileExtension(request.File.Name)) 
                    return Task.FromResult(ProcessFileResponse.ErrorResponse($"Invalid file extension received, allowed extensions: {Constants.ValidFileExtensions}"));

                var operationLines = _getOperationLines.GetLines(request.File.Data);
                if (!operationLines.Any()) return Task.FromResult(ProcessFileResponse.ErrorResponse("The file does not contain valid fields"));
                
                var applyOperation = operationLines.Last();
                if (!Enum.TryParse(applyOperation.OperationName, true, out OperationType lastOperationType) || lastOperationType != OperationType.Apply)
                    return Task.FromResult(ProcessFileResponse.ErrorResponse($"The last operation must be of type {OperationType.Apply}"));
                
                operationLines.Remove(applyOperation);

                var operationValue = applyOperation.Value;
                foreach (var operationLine in operationLines.OrderBy(ol => ol.Order))
                {
                    var operationToApply = _operationFactory.GetCurrentOperation(operationLine.OperationName);
                    operationValue = operationToApply.ExecuteOperation(operationValue, operationLine.Value);
                }
                return Task.FromResult(ProcessFileResponse.SuccessResponse(operationValue.ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Class}({Method}): {Message}", nameof(ProcessFileHandler), nameof(Handle), ex.Message);
                return Task.FromResult(ProcessFileResponse.ErrorResponse($"Error processing file {request.File.Name}, with error message: {ex.Message}"));
            }
        }

        private bool ValidateFileExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName), "The file name cannot be null or empty");
            var fileExtension = Path.GetExtension(fileName);
            var validFileExtensions = Constants.ValidFileExtensions.Split(';');
            return validFileExtensions.Any(fe => fe == fileExtension);
        }
    }
}