namespace MormorDB.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string ArticleNumber { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }

    public double WeightGrams { get; set; }
    public int PackageQuantity { get; set; }
    public DateTime BestBefore { get; set; }
    public DateTime ManufacturedDate { get; set; }


    public List<SupplierProduct> SupplierProducts { get; set; }
     public List<OrderItem> OrderItems { get; set; }
}
