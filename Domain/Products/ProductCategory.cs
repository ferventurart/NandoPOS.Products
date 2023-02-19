namespace Domain.Products;

public class ProductCategory
{
    public ProductCategory(string name)
    {
        Name = name;
        Active = true;
    }

    public Guid Id { get; init; }

    public string Name { get; set; }

    public bool Active { get; set; }

    public void ChangeStatus() => Active = !Active;

    public void ChangeName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name) && Name != name)
        {
            Name = name;
        }
    }
}
