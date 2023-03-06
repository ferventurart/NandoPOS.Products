using Domain.Abstractions;
using MediatR;

namespace Domain.Products;

public class Product : AggregateRoot
{
    public Product(
        string? barcode,
        string? sku,
        string name,
        string? description,
        decimal cost,
        decimal price,
        bool useInventory,
        decimal? stockMin,
        decimal? stockMax,
        ProductCategory productCategory)
    {
        Barcode = barcode;
        Sku = sku;
        Name = name;
        Description = description;
        Cost = cost;
        Price = price;
        Earn = Price - Cost;
        UseInventory = useInventory;
        StockMin = stockMin;
        StockMax = stockMax;
        Sizes = new();
        Colors = new();
        Taxes = new();
        ProductCategory = productCategory;
        Active = true;
    }

    public Guid Id { get; init; }

    public string? Barcode { get; set; }

    public string? Sku { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal Cost { get; set; }

    public decimal Price { get; set; }

    public decimal Earn { get; set; }

    public string? Image { get; set; }

    public bool UseInventory { get; set; }

    public decimal? StockMin { get; set; }

    public decimal? StockMax { get; set; }

    public List<string> Sizes { get; set; }

    public List<string> Colors { get; set; }

    public ProductCategory ProductCategory { get; set; }

    public List<Tax> Taxes { get; set; }

    public bool Active { get; set; }

    public void ChangeStatus() => Active = !Active;

    public void ChangeUseInventory() => UseInventory = !UseInventory;

    public void ChangeBarcode(string? barcode)
    {
        if (!string.IsNullOrWhiteSpace(barcode) && Barcode != barcode)
        {
            Barcode = barcode;
        }
    }

    public void ChangeSku(string? sku)
    {
        if (!string.IsNullOrWhiteSpace(sku) && Sku != sku)
        {
            Sku = sku;
        }
    }

    public void ChangeName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name) && Name != name)
        {
            Name = name;
        }
    }

    public void ChangeDescription(string? description)
    {
        if (!string.IsNullOrWhiteSpace(description) && Description != description)
        {
            Description = description;
        }
    }

    public void ChangeCost(decimal cost)
    {
        if (Cost != cost && cost < Price && cost > 0)
        {
            Cost = cost;
        }
    }

    public void ChangePrice(decimal price)
    {
        if (Price != price && price > Cost && price > 0)
        {
            Price = price;
        }
    }

    public void ChangeStockMin(decimal? stockMin)
    {
        if (stockMin is not null && StockMin != stockMin && StockMax is not null && stockMin < StockMax)
        {
            StockMin = stockMin;
        }
    }

    public void ChangeStockMax(decimal? stockMax)
    {
        if (stockMax is not null && StockMax != stockMax && StockMin is not null && stockMax > StockMin)
        {
            StockMax = stockMax;
        }
    }

    public void CalculateEarn() => Earn = Price - Cost;

    public void AddSizes(List<string> sizes)
    {
        if (sizes.Count > 0)
        {
            foreach (var size in sizes)
            {
                AddSize(size);
            }
        }
    }

    public void AddColors(List<string> colors)
    {
        if (colors.Count > 0)
        {
            foreach (var color in colors)
            {
                AddColor(color);
            }
        }
    }

    public void AddTaxes(IReadOnlyList<Tax>? taxes, List<Guid> taxesId)
    {
        if (taxes?.Count > 0 && taxesId.Count > 0)
        {
            foreach (var id in taxesId)
            {
                var tax = taxes.First(t => t.Id == id);
                if (tax is not null)
                {
                    AddTax(tax);
                }
            }
        }
    }

    public void ChangeProductCategory(ProductCategory productCategory)
    {
        if (ProductCategory != productCategory)
        {
            ProductCategory = productCategory;
        }
    }

    private void AddTax(Tax tax)
    {
        if (tax is not null && !Taxes.Any(s => s.Id == tax.Id))
        {
            Taxes.Add(tax);
        }
    }

    private void AddSize(string size)
    {
        if (!string.IsNullOrEmpty(size) && size.Length <= 3 && !Sizes.Any(s => s == size))
        {
            Sizes.Add(size);
        }
    }

    private void AddColor(string color)
    {
        if (!string.IsNullOrEmpty(color) && color.Length <= 6 && !Colors.Any(s => s == color))
        {
            Colors.Add(color);
        }
    }
}
