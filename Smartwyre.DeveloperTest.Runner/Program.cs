using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using System;

// Create a Service Collection
IServiceCollection services = new ServiceCollection();

// Register necessary services
services.AddSingleton<ILogger, ConsoleLogger>();
services.AddSingleton<IRebateService, RebateService>();
services.AddSingleton<IRebateDataStore, RebateDataStore>();
services.AddSingleton<IProductDataStore, ProductDataStore>();
services.AddSingleton<ICalculatorFactory, CalculatorFactory>();

// Build the services
var build = services.BuildServiceProvider();

// Get the needed services
var rebateService = build.GetRequiredService<IRebateService>();
var logger = build.GetRequiredService<ILogger>();


logger.Info(@"Example calculation ""--rebateid 54 --productid 104 --volume 3""");

// Watch for input and then act on it
while (true)
{
    var input = Console.ReadLine();

    // Create the request and parse the input
    CalculateRebateRequest request = new(input);
    // Valid that the input was valid
    if (request.IsValid())
    {
        // Run the Calculation
        var rebateResult = rebateService.Calculate(request);

        // Return the response
        if (rebateResult.Success)
            logger.Log("Complete!");
        else
            logger.Warning("This product not eligible for the entered rebate!");
    }
    else
        logger.Error("Invalid Params given Please use --rebateid(string), --productid(string), --volume(int)");
}