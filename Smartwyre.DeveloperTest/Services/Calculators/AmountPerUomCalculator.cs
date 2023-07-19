using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators;

public class AmountPerUomCalculator : IRebateCalculator
{
    public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Incentive is IncentiveType.AmountPerUom &&
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) &&
               rebate.Amount > 0 && request.Volume > 0;
    }

    public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount * request.Volume;
    }
}
