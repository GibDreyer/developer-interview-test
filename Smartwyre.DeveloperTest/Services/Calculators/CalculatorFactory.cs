using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services.Calculators;

public class CalculatorFactory : ICalculatorFactory
{
    private readonly List<IRebateCalculator> _calculators;

    public CalculatorFactory()
    {
       // Build a list of calculators
        _calculators = new List<IRebateCalculator>
            {
                new FixedCashAmountCalculator(),
                new FixedRateRebateCalculator(),
                new AmountPerUomCalculator()
            };
    }

    public IRebateCalculator? GetCalculator(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        // return the calculator that can calculate the rebate amount based on the given params
        return _calculators.FirstOrDefault(x => x.CanCalculate(rebate, product, request));
    }
}