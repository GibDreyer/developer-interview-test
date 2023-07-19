using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public Product GetProduct(string productIdentifier)
    {
        // Return a random product that is eligible for 2 rebates making it eligible 66% of the time
        // Create mock data
        Random random = new();

        Product product = new()
        {
            Identifier = productIdentifier,
            Id = random.Next(1, 100),
            Price = random.Next(1, 100),
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.FixedCashAmount,
            Uom = Guid.NewGuid().ToString()
        };

        return product;
    }
}
