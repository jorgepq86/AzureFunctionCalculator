using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CalculatorFunction.Model;
using CalculatorFunction.Model.Request;
using CalculatorFunction.Model.Response;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Calculator.BusinessLogic.Tests
{
    public class WhenReceiveAnEmptyFile : TesForProcessFileHandler
    {
        private ProcessFileResponse _processFileRequest;
        protected override async Task WhenAsync()
        {
            var newBlobFile = new BlobFile("EmptyFile.txt", null);
            var newProcessFileRequest = new ProcessFileRequest(newBlobFile);
            _processFileRequest = await Subject.Handle(newProcessFileRequest, CancellationToken.None);
        }

        protected override Task SetupAsync()
        {
            GetOperationLinesMock.Setup(ol => ol.GetLines(It.IsAny<Stream>())).Returns(new List<OperationLine>());
            return Task.CompletedTask;
        }

        protected override void TearDown()
        {
            GetOperationLinesMock.Invocations.Clear();
        }

        [Test]
        public void ThenProcessStatusShouldBeFalse() =>
            _processFileRequest.Status.Should().BeFalse();

        [Test]
        public void ThenMessageShouldBe() =>
            _processFileRequest.Message.Should().Contain("The file does not contain valid fields");

        [Test]
        public void ThenGetOperationLinesShouldBeCalled() =>
            GetOperationLinesMock.Verify(ol => ol.GetLines(It.IsAny<Stream>()), Times.Once);
    }
}