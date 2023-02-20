namespace Domain.Extensions;

public static class ContainsDuplicate
{
    public static bool Any<T>(IEnumerable<T> enumerable)
    {
        HashSet<T> set = new();

        return enumerable.Any(e => !set.Add(e));
    }
}
