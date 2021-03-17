using System;
using System.IO;
using System.Threading.Tasks;
using CalculatorFunction.Model;
using CalculatorFunction.Model.Request;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CalculatorFunction.Functions
{
    public class CalculatorBlobTrigger
    {
        private readonly IMediator _mediator;

        public CalculatorBlobTrigger(IMediator mediator)
        {
            _mediator = mediator;
        }


        [FunctionName("operationsFile")]
        public async Task Run([BlobTrigger("calculator/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            try
            {
                var blobFile = new BlobFile(name, myBlob);
                var calculatorResponse = await _mediator.Send(new ProcessFileRequest(blobFile)).ConfigureAwait(false);
                log.LogInformation("Operation result: {Status}. Result: {Result}", calculatorResponse.Status, calculatorResponse.Message);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error in function:{FunctionName} with message: {Message}", "operationsFile", ex.Message);
            }
        }
    }
}
