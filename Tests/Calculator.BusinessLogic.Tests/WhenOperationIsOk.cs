using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CalculatorFunction.Model;
using CalculatorFunction.Model.Request;
using CalculatorFunction.Model.Response;
using CalculatorFunction.Operations.Abstractions;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Calculator.BusinessLogic.Tests
{
    public class WhenOperationIsOk : TesForProcessFileHandler
    {
        private ProcessFileResponse _processFileRequest;
        private readonly Mock<IOperation> _operationMock = new Mock<IOperation>();
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
                    Value = 5,
                    Order = 0
                },
                new OperationLine
                {
                    OperationName = "subtract",
                    Value = 3,
                    Order = 1
                },
                new OperationLine
                {
                    OperationName = "divide",
                    Value = 4,
                    Order = 2
                },
                new OperationLine
                {
                    OperationName = "multiply",
                    Value = 3,
                    Order = 2
                },
                new OperationLine
                {
                    OperationName = "apply",
                    Value = 10,
                    Order = 3
                }
            };
            GetOperationLinesMock.Setup(ol => ol.GetLines(It.IsAny<Stream>())).Returns(newOperationLines);

            _operationMock.SetupSequence(o => o.ExecuteOperation(It.IsAny<double>(), It.IsAny<double>()))
                .Returns(15)
                .Returns(12)
                .Returns(3)
                .Returns(9);

            OperationFactoryMock.Setup(of => of.GetCurrentOperation(It.IsAny<string>())).Returns(_operationMock.Object);
            
            return Task.CompletedTask;
        }

        protected override void TearDown()
        {
            GetOperationLinesMock.Invocations.Clear();
            OperationFactoryMock.Invocations.Clear();
            _operationMock.Invocations.Clear();
        }

        [Test]
        public void ThenProcessStatusShouldBeTrue() =>
            _processFileRequest.Status.Should().BeTrue();

        [Test]
        public void ThenMessageShouldBe() =>
            _processFileRequest.Message.Should().Contain("9");

        [Test]
        public void ThenMocksShouldBeCalled()
        {
            GetOperationLinesMock.Verify(ol => ol.GetLines(It.IsAny<Stream>()), Times.Once);
            OperationFactoryMock.Verify(of => of.GetCurrentOperation(It.IsAny<string>()), Times.Exactly(4));
            _operationMock.Verify(o => o.ExecuteOperation(It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(4));
        }
    }
}