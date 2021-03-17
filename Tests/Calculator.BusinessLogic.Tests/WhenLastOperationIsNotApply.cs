using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CalculatorFunction.Model;
using CalculatorFunction.Model.Enums;
using CalculatorFunction.Model.Request;
using CalculatorFunction.Model.Response;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Calculator.BusinessLogic.Tests
{
    public class WhenLastOperationIsNotApply : TesForProcessFileHandler
    {
        private ProcessFileResponse _processFileRequest;
        protected override async Task WhenAsync()
        {
            var newBlobFile = new BlobFile("NotApply.txt", null);
            var newProcessFileRequest = new ProcessFileRequest(newBlobFile);
            _processFileRequest = await Subject.Handle(newProcessFileRequest, CancellationToken.None);
        }

        protected override Task SetupAsync()
        {
            var newOperationLines = new List<OperationLine>()
            {
                new OperationLine
                {
                    OperationName = "add",
                    Value = 1,
                    Order = 0
                },
                new OperationLine
                {
                    OperationName = "subtract",
                    Value = 1,
                    Order = 1
                },
                new OperationLine
                {
                    OperationName = "divide",
                    Value = 1,
                    Order = 2
                },
                new OperationLine
                {
                    OperationName = "multiply",
                    Value = 1,
                    Order = 3
                }
            };
            GetOperationLinesMock.Setup(ol => ol.GetLines(It.IsAny<Stream>())).Returns(newOperationLines);
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
            _processFileRequest.Message.Should().Contain($"The last operation must be of type {OperationType.Apply}");

        [Test]
        public void ThenGetOperationLinesShouldBeCalled() =>
            GetOperationLinesMock.Verify(ol => ol.GetLines(It.IsAny<Stream>()), Times.Once);
    }
}