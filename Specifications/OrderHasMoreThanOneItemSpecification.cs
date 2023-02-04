namespace TheOmenDen.Shared.Tests.Specifications;
internal sealed record OrderHasMoreThanOneItemSpecification: Specification<Order>
{
    private const int OrderCountThreshold = 1;

    public override Expression<Func<Order, bool>> ToExpression() => order => order.Quantity > OrderCountThreshold;
}
