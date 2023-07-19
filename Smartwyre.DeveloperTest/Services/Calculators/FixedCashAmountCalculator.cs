using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators;

public class FixedCashAmountCalculator : IRebateCalculator
{
    public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Incentive is IncentiveType.FixedCashAmount &&
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) &&
               rebate.Amount > 0;
    }

    public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount;
    }
}