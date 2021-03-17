using CalculatorFunction.Model.Response;
using MediatR;

namespace CalculatorFunction.Model.Request
{
    public class ProcessFileRequest : IRequest<ProcessFileResponse>
    {
        public BlobFile File { get; set; }

        public ProcessFileRequest(BlobFile file)
        {
            File = file;
        }
    }
}