using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;

namespace PseudoRMI_CalculatorServer
{
    public class ServerMain
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure ASP.NET Core to listen on all interfaces (0.0.0.0) and port 8080
            builder.WebHost.UseUrls("http://0.0.0.0:8080");

            // Add support for WCF services
            builder.Services.AddServiceModelServices();
            builder.Services.AddServiceModelMetadata(); // This already adds ServiceMetadataBehavior

            // Register the calculator service
            builder.Services.AddSingleton<ICalculatorService, CalculatorService>();

            var app = builder.Build();

            // Configure the WCF service
            app.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<CalculatorService>();

                serviceBuilder.AddServiceEndpoint<CalculatorService, ICalculatorService>(
                    new BasicHttpBinding(),
                    "/CalculatorService"
                );

                // Retrieve the existing ServiceMetadataBehavior and modify it
                var metadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
                metadataBehavior.HttpGetEnabled = true;
                metadataBehavior.HttpsGetEnabled = false;
                metadataBehavior.HttpGetUrl = new Uri("http://192.168.50.183:8080/CalculatorService/mex"); // Server IP or 0.0.0.0 for all interfaces
            });

            // Run the application
            app.Run();
        }
    }
}
