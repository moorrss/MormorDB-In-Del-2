namespace MormorDB.Entities;

public class Order
{
public int OrderId { get; set; }
public string OrderNumber { get; set; }
public DateTime OrderDate { get; set; }

public int CustomerId { get; set; }
public Customer Customer { get; set; }

public List<OrderItem> OrderLines { get; set; } = [];
}
