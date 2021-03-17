using System.Threading.Tasks;
using Calculator.BusinessLogic.Handlers;
using CalculatorFunction.Operations.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Calculator.BusinessLogic.Tests
{
    [TestFixture]
    public abstract class TesForProcessFileHandler
    {
        protected readonly Mock<IOperationFactory> OperationFactoryMock = new Mock<IOperationFactory>();
        protected readonly Mock<ILogger<ProcessFileHandler>> LoggerMock = new Mock<ILogger<ProcessFileHandler>>();
        protected readonly Mock<IGetOperationLines> GetOperationLinesMock = new Mock<IGetOperationLines>();

        protected ProcessFileHandler Subject { get; set; }

        [SetUp]
        protected async Task TestForSetupAsync()
        {
            await SetupAsync().ConfigureAwait(false);
            Subject = await GivenAsync().ConfigureAwait(false);
            await WhenAsync().ConfigureAwait(false);
        }

        protected virtual Task SetupAsync() => Task.CompletedTask;

        private Task<ProcessFileHandler> GivenAsync()
        {
            var newProcessFileHandler = new ProcessFileHandler(OperationFactoryMock.Object, LoggerMock.Object, GetOperationLinesMock.Object);
            return Task.FromResult(newProcessFileHandler);
        }

        protected abstract Task WhenAsync();

        [TearDown]
        protected virtual void TearDown()
        {
        }
    }
}