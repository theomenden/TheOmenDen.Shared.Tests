namespace TheOmenDen.Shared.Tests.Specifications;
internal sealed class Order
{
        public int OrderId { get; set; }
        public Fruit Item { get; set; }
        public int Quantity { get; set; }
        public int? LotNumber { get; set; }
}