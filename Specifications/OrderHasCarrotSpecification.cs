namespace TheOmenDen.Shared.Tests.Specifications;
internal sealed record OrderHasCarrotSpecification: Specification<Order>
{
    private const string SearchItem = "carrot";
    
    public override Expression<Func<Order, bool>> ToExpression() =>  order => order.Item.Name.Equals(SearchItem, StringComparison.OrdinalIgnoreCase);
}
