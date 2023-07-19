using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Create random mock data giving it a random incentive type
        Random random = new();

        Rebate rebate = new()
        {
            Identifier = rebateIdentifier,
            Amount = random.Next(1, 100),
            Incentive = (IncentiveType)random.Next(0, Enum.GetNames(typeof(IncentiveType)).Length),
            Percentage = (decimal)random.NextDouble()
        };

        return rebate;
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }
}
