using System.Collections.Generic;

public static class CollectionHelpers
{
    public static HashSet<T> Add<T>(this HashSet<T> hashSet, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            hashSet.Add(item);
        }
        return hashSet;
    }
}