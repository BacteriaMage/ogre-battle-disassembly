// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Extensions;

public static class ListExtensions
{
    public static void Enqueue<T>(this List<T> list, T item)
    {
        list.Add(item);
    }
   
    public static T Dequeue<T>(this List<T> list)
    {
        var item = list[0];
        list.RemoveAt(0);
        return item;
    }

    public static T? DequeueOrDefault<T>(this List<T> list)
    {
        return list.IsNotEmpty() ? Dequeue<T>(list) : default;
    }

    public static void Push<T>(this List<T> list, T item)
    {
        list.Add(item);
    }
    
    public static T Pop<T>(this List<T> list)
    {
        var lastIndex = list.GetLastIndex();
        var item = list[lastIndex];
        list.RemoveAt(lastIndex);
        return item;
    }
    
    public static T? PopOrDefault<T>(this List<T> list)
    {
        return list.IsNotEmpty() ? Pop<T>(list) : default;
    }

    public static bool IsEmpty<T>(this List<T> list)
    {
        return list.Count == 0;
    }

    public static bool IsNotEmpty<T>(this List<T> list)
    {
        return list.Count > 0;
    }

    public static int GetLastIndex<T>(this List<T> list)
    {
        if (list.IsEmpty())
        {
            throw new InvalidOperationException("List is empty");
        }

        return list.Count - 1;
    }
}