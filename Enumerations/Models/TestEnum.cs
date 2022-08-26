
namespace TheOmenDen.Shared.Tests.Enumerations.Models;
public record TestEnumeration : EnumerationBase<TestEnumeration>
{
    public static readonly TestEnumeration Option1 = new(nameof(Option1),1);
    public static readonly TestEnumeration Option2 = new(nameof(Option2), 2);
    public static readonly TestEnumeration Option3 = new(nameof(Option3), 3);
    protected TestEnumeration(string name, int id) : base(name, id)
    {}
}