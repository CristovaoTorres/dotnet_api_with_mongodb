namespace DotNetExample_API.Domain.Request;

public class ProductUpdateRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product MapToUpdate()
    {
        return new Product { Id = this.Id, Name = this.Name, Price = this.Price, Quantity = this.Quantity };
    }
}
