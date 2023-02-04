namespace TheOmenDen.Shared.Tests.Specifications;
internal sealed record OrderHasLemonSpecification: Specification<Order>
{
    public override Expression<Func<Order, bool>> ToExpression() => order => order.Item == Fruit.Lemon;
}
