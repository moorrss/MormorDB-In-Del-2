namespace MormorDB.Entities;

public class Supplier
{
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactPerson { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public List<SupplierProduct> SupplierProducts { get; set; }
}
