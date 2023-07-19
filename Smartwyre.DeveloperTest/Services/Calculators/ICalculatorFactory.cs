using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public interface ICalculatorFactory
    {
        IRebateCalculator? GetCalculator(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}