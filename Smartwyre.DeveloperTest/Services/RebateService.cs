using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public partial class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly ICalculatorFactory _calculatorFactory;
    public RebateService(IRebateDataStore rebateData, IProductDataStore productData,
        ICalculatorFactory calculatorFactory)
    {
        _productDataStore = productData;
        _rebateDataStore = rebateData;
        _calculatorFactory = calculatorFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        CalculateRebateResult result = new() { Success = false };

        if (!request.IsValid())
            return result;

        // Get the required information
        Rebate? rebate = _rebateDataStore.GetRebate(request.RebateIdentifier!);
        Product? product = _productDataStore.GetProduct(request.ProductIdentifier!);

        // If the product or rebate is not found return false;
        if (product is null || rebate is null)
            return result;

        // Get the correct calculator for the given request
        var calculator = _calculatorFactory.GetCalculator(rebate, product, request);
        if (calculator != null)
        {
            // Calculate the rebate amount;
            decimal rebateAmount = calculator.CalculateRebateAmount(rebate, product, request);
            // Update the rebate amount in the database
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

            result.Success = true;
        }
        else
        {
            result.Success = false;
        }

        return result;
    }
}