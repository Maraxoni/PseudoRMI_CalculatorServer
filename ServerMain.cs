using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PseudoRMI_CalculatorServer
{
    public class ServerMain
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddServiceModelServices();
            builder.Services.AddServiceModelMetadata();
            builder.Services.AddSingleton<ICalculatorService, CalculatorService>(); // Rejestracja serwisu

            var host = builder.Build();
            host.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<CalculatorService>()
                              .AddServiceEndpoint<CalculatorService, ICalculatorService>(
                                  new BasicHttpBinding(), "/CalculatorService");
            });

            host.Run();
        }
    }
}
