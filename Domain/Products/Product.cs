using Domain.Abstractions;

namespace Domain.Products;

public class Product : AggregateRoot
{
    public Product(string?barcode, string? sku, string name, decimal cost, decimal price, List<string>? sizes, List<string>? colors, ProductCategory productCategory, List<Tax> taxes)
    {
        Barcode = barcode;
        Sku = sku;
        Name = name;
        Cost = cost;
        Price = price;
        Earn = Price - Cost;
        Sizes = sizes;
        Colors = colors;
        ProductCategory = productCategory;
        Taxes = taxes;
        Active = true;
    }

    public Guid Id { get; init; }

    public string? Barcode { get; set; }

    public string? Sku { get; set; }

    public string Name { get; set; }

    public decimal Cost { get; set; }

    public decimal Price { get; set; }

    public decimal Earn { get; set; }

    public string? Image { get; set; }

    public List<string>? Sizes { get; set; }

    public List<string>? Colors { get; set; }

    public ProductCategory ProductCategory { get; set; }

    public List<Tax> Taxes { get; set; }

    public bool Active { get; set; }

    public void Inactive() => Active = false;
}
