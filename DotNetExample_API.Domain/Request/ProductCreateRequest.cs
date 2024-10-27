namespace DotNetExample_API.Domain.Request;

public class ProductCreateRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product MapToCreate()
    {
        return new Product { Name = this.Name, Price = this.Price, Quantity = this.Quantity };
    }
}
