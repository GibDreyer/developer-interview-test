using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;
using Moq;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void Calculate_WithFixedCashAmount_ReturnsSuccessResult()
    {
        // Arrange
        var rebate = new Rebate
        {
             Identifier = "1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 50
        };
        var product = new Product
        {
            Identifier = "100",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var rebateCalculator = MockRebateService(rebate, product);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 10
        };

        // Act
        var result = rebateCalculator.Calculate(request);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void Calculate_WithFixedRateRebate_ReturnsSuccessResult()
    {
        // Arrange
        var rebate = new Rebate
        {
             Identifier = "2",
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 0.1m
        };
        var product = new Product
        {
             Identifier = "101",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 100
        };

        var rebateCalculator = MockRebateService(rebate, product);
        
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 5
        };

        // Act
        var result = rebateCalculator.Calculate(request);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void Calculate_WithAmountPerUom_ReturnsSuccessResult()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5
        };
        var product = new Product
        {
            Identifier = "102",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var rebateCalculator = MockRebateService(rebate, product);
        
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 8
        };

        // Act
        var result = rebateCalculator.Calculate(request);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void Calculate_WithUnsupportedIncentive_ReturnsFailureResult()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "4",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100
        };
        var product = new Product
        {
            Identifier = "103",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var rebateCalculator = MockRebateService(rebate, product);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 5
        };

        // Act
        var result = rebateCalculator.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void Calculate_WithInvalidData_ReturnsFailureResult()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "5",
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 0.1m
        };
        var product = new Product
        {
            Identifier = "104",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 0 // Invalid product price
        };

        var rebateCalculator = MockRebateService(rebate, product);
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            Volume = 0
        };

        // Act
        var result = rebateCalculator.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    private static RebateService MockRebateService(Rebate rebate, Product product)
    {
        // Create a mock of the rebate data store
        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        rebateDataStoreMock.Setup(d => d.GetRebate(rebate.Identifier)).Returns(rebate);

        // Create a mock of the product data store
        var productDataStoreMock = new Mock<IProductDataStore>();
        productDataStoreMock.Setup(d => d.GetProduct(product.Identifier)).Returns(product);

        // Create a calculator factory
        ICalculatorFactory calculatorFactory = new CalculatorFactory();

        // Return the rebate service with the mocked data stores
        return new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, calculatorFactory);
    }
}
