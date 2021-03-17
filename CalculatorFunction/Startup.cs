using Calculator.BusinessLogic.Handlers;
using CalculatorFunction;
using CalculatorFunction.Operations;
using CalculatorFunction.Operations.Abstractions;
using CalculatorFunction.Operations.Factory;
using CalculatorFunction.Operations.Reader;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace CalculatorFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Mediator
            builder.Services.AddMediatR(typeof(ProcessFileHandler));
            
            //IoC
            builder.Services.AddScoped<IGetOperationLines, GetOperationLines>();
            builder.Services.AddScoped<IOperationFactory, OperationFactory>();
            builder.Services.AddScoped<IOperation, AddOperation>();
            builder.Services.AddScoped<IOperation, SubtractOperation>();
            builder.Services.AddScoped<IOperation, MultiplyOperation>();
            builder.Services.AddScoped<IOperation, DivideOperation>();
        }
    }
}