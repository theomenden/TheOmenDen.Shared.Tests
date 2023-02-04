using Bogus;

namespace TheOmenDen.Shared.Tests.Specifications;
internal static class OrderGenerator
{
    public static Faker<Order> CreateRulesForFakeOrders()
    => new Faker<Order>()
            .RuleFor(o => o.OrderId, f => f.IndexFaker)
            .RuleFor(o => o.Item, Fruit.ReadOnlyEnumerationList.Random())
            .RuleFor(o => o.Quantity, f => f.Random.Number(1, 100))
            .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .8f));

}
