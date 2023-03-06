namespace Domain.Products;

public class Tax
{
    public Tax(
        string name,
        string shortName,
        decimal percentage)
    {
        Name = name;
        ShortName = shortName;
        Percentage = percentage;
    }

    public Guid Id { get; init; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public decimal Percentage { get; set; }

    public void ChangeName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name) && Name != name)
        {
            Name = name;
        }
    }

    public void ChangeShortName(string shortName)
    {
        if (!string.IsNullOrWhiteSpace(shortName) && ShortName != shortName)
        {
            ShortName = shortName;
        }
    }

    public void ChangePercentage(decimal percentage) => Percentage = percentage;
}
