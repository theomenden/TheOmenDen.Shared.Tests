namespace TheOmenDen.Shared.Tests.Specifications;

internal record Fruit : EnumerationBase<Fruit>
{
    private Fruit(string name, int id) : base(name, id)
    {}

    public static readonly Fruit Lemon = new(nameof(Lemon), 1);
    public static readonly Fruit Lime = new(nameof(Lime), 2);
    public static readonly Fruit Mango = new(nameof(Mango), 3);
    public static readonly Fruit Starfruit = new(nameof(Starfruit), 4);
    public static readonly Fruit Papaya = new(nameof(Papaya), 5);
    public static readonly Fruit Guava = new(nameof(Guava), 6);
    public static readonly Fruit Apple = new(nameof(Apple), 7);
    public static readonly Fruit Orange = new(nameof(Orange), 8);
    public static readonly Fruit Strawberry = new(nameof(Strawberry), 9);
    public static readonly Fruit Grape = new(nameof(Grape), 10);
}