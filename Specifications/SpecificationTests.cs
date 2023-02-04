namespace TheOmenDen.Shared.Tests.Specifications;
public class SpecificationTests
{
    [Fact]
    public void CompiledExpressions_ShouldReturnResultsWhenTheGivenConditionIsMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var expectedResults = orders.Where(o => o.Item.Equals(Fruit.Lemon)).ToList();
        var orderContainsLemonSpec = new OrderHasLemonSpecification();
        
        //Act
        var actualResults = orders
            .Where(orderContainsLemonSpec.ToExpression().Compile())
            .ToList();
        
        //Assert
        actualResults.Count.ShouldBe(expectedResults.Count);
    }

    [Fact]
    public void CompiledExpressions_ShouldReturnNoResultsWhenTheGivenConditionIsNotMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var orderContainsCarrotSpec = new OrderHasCarrotSpecification();

        //Act
        var actualResults = orders
            .Where(orderContainsCarrotSpec.ToExpression().Compile())
            .ToList();

        //Assert
        actualResults.ShouldBeEmpty();
    }

    [Fact]
    public void IsSatisfiedBy_ShouldReturnTrueWhenTheGivenConditionIsMet()
    {
        //Arrange
        var order = new Order
        {
            OrderId = 1,
            Item = Fruit.Lemon,
            LotNumber = 1,
            Quantity = 2
        };

        var orderContainsLemonSpec = new OrderHasLemonSpecification();

        //Act
        var result = orderContainsLemonSpec.IsSatisfiedBy(order);

        //Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsSatisfiedBy_ShouldReturnFalseWhenTheGivenConditionIsNotMet()
    {
        //Arrange
        var order = new Order
        {
            OrderId = 1,
            Item = Fruit.Lemon,
            LotNumber = 1,
            Quantity = 2
        };

        var orderContainsCarrotSpec = new OrderHasCarrotSpecification();

        //Act
        var result = orderContainsCarrotSpec.IsSatisfiedBy(order);

        //Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void Not_ShouldReturnFalseWhenTheGivenConditionIsMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var orderContainsCarrotSpec = new OrderHasCarrotSpecification();

        //Act
        var actualResults = orders
            .Where(orderContainsCarrotSpec.Not().ToExpression().Compile())
            .ToList();

        //Assert
        actualResults.ShouldNotBeEmpty();
    }

    [Fact]
    public void Not_ShouldReturnFalseWhenTheGivenConditionIsNotMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var orderContainsLemonSpec = new OrderHasLemonSpecification();

        var expectedResults = orders.Where(o => o.Item != Fruit.Lemon).ToList();

        //Act
        var actualResults = orders
            .Where(orderContainsLemonSpec.Not()
                .ToExpression()
                .Compile())
            .ToList();

        //Assert
        actualResults.Count.ShouldBe(expectedResults.Count);
    }

    [Fact]
    public void And_ShouldBeSatisfiedWhenTheGivenConditionsAreMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var orderContainsLemonSpec = new OrderHasLemonSpecification();
        var orderHasMoreThanOneItemSpec = new OrderHasMoreThanOneItemSpecification();
        
        var expectedResults = orders.Where(o => o.Item == Fruit.Lemon && o.Quantity > 1).ToList();

        //Act
        var actualResults = orders
            .Where(orderContainsLemonSpec.And(orderHasMoreThanOneItemSpec)
                .ToExpression()
                .Compile())
            .ToList();

        //Assert
        actualResults.Count.ShouldBe(expectedResults.Count);
    }

    [Fact]
    public void Or_ShouldBeSatisfiedWhenEitherOfTheGivenConditionsAreMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var orderContainsLemonSpec = new OrderHasLemonSpecification();
        var orderHasMoreThanOneItemSpec = new OrderHasMoreThanOneItemSpecification();

        var expectedResults = orders.Where(o => o.Item == Fruit.Lemon || o.Quantity <= 1).ToList();

        //Act
        var actualResults = orders
            .Where(orderContainsLemonSpec.Or(orderHasMoreThanOneItemSpec.Not())
                .ToExpression()
                .Compile())
            .ToList();

        //Assert
        actualResults.Count.ShouldBe(expectedResults.Count);
    }

    [Fact]
    public void Nand_ShouldBeSatisfiedWhenEitherOfTheGivenConditionsAreMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var orderContainsLemonSpec = new OrderHasLemonSpecification();
        var orderHasMoreThanOneItemSpec = new OrderHasMoreThanOneItemSpecification();

        var expectedResults = orders.Where(o => o.Item != Fruit.Lemon || o.Quantity <= 1).ToList();

        //Act
        var actualResults = orders
            .Where(orderContainsLemonSpec.Nand(orderHasMoreThanOneItemSpec)
                .ToExpression()
                .Compile())
            .ToList();

        //Assert
        actualResults.Count.ShouldBe(expectedResults.Count);
    }

    [Fact]
    public void Nor_ShouldBeSatisfiedWhenNeitherOfTheGivenConditionsAreMet()
    {
        //Arrange
        var orders = OrderGenerator.CreateRulesForFakeOrders().Generate(RandomData.GetInt(1, 100));
        var orderContainsLemonSpec = new OrderHasCarrotSpecification();
        var orderHasMoreThanOneItemSpec = new OrderHasMoreThanOneItemSpecification();

        var expectedResults = orders.Where(o => 
                !o.Item.Name.Equals("carrot", StringComparison.OrdinalIgnoreCase)
                && o.Quantity <= 1)
            .ToList();

        //Act
        var actualResults = orders
            .Where(orderContainsLemonSpec.Nor(orderHasMoreThanOneItemSpec)
                .ToExpression()
                .Compile())
            .ToList();

        //Assert
        actualResults.Count.ShouldBe(expectedResults.Count);
    }
}
