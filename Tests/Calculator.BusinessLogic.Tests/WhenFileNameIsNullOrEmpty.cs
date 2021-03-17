using System.Threading;
using System.Threading.Tasks;
using CalculatorFunction.Model;
using CalculatorFunction.Model.Request;
using CalculatorFunction.Model.Response;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.BusinessLogic.Tests
{
    public class WhenFileNameIsNullOrEmpty : TesForProcessFileHandler
    {
        private ProcessFileResponse _processFileRequest;

        protected override async Task WhenAsync()
        {
            var newBlobFile = new BlobFile("", null);
            var newProcessFileRequest = new ProcessFileRequest(newBlobFile);
            _processFileRequest = await Subject.Handle(newProcessFileRequest, CancellationToken.None);
        }

        [Test]
        public void ThenProcessStatusShouldBeFalse() =>
            _processFileRequest.Status.Should().BeFalse();

        [Test]
        public void ThenMessageShouldBe() =>
            _processFileRequest.Message.Should().Contain("The file name cannot be null or empty");
        
    }
}