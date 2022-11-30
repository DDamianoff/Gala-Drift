internal static class Assist
{
    private static bool _previouslyUsed;
    
    public static void Print(this IEnumerable<Book> books, string text)
    {
        WriteTitle(text);
        WriteData(books);
    }

    private static void WriteTitle(string text)
    {
        Separate();

        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine(">> ");
        Console.Write(">> ");
        Console.WriteLine(text, Environment.NewLine);
        Console.WriteLine(">> ");
        Console.ResetColor();
    }
    
    private static void WriteTinyTitle(string text)
    {
        Separate();

        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        
        Console.Write(">> ");
        Console.WriteLine(text, Environment.NewLine);
        Console.ResetColor();
    }

    public static void Print<TKey>(this IEnumerable<IGrouping<TKey, Book>> groupedList, string title)
    {
        WriteTitle(title);
        foreach (var group in groupedList)
        {
            WriteTinyTitle($"Group: {group.Key}");
            WriteData(group.ToList());
        }
    }
    
    
    
    public static void Print(this IEnumerable<Book> books)
    {
        Separate();
        WriteData(books);
    }

    private static void WriteData(IEnumerable<Book> books)
    {
        var header = $"{"Book Title",-60} {"| Page Count",15} {"| Publish Date",15}";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(header);
        Console.ResetColor();
        
        Console.WriteLine("".PadRight(header.Length,'—'));
        
        foreach (var book in books)
            Console.WriteLine($"{book.Title,-60} {book.PageCount,15} {book.PublishedDate.ToShortDateString(),15}");
        
        Console.WriteLine("".PadRight(header.Length,'—'));
    }

    public static void Print(this object stuff, string text) 
        => WriteTitle($"{text}: {stuff}");


    private static void Separate()
    {
        if (_previouslyUsed) 
            Console.WriteLine(Environment.NewLine, Environment.NewLine, Environment.NewLine);
        
        _previouslyUsed = true;
    }
    public static string Normalized(this string input) => input.ToLower();
    
    public static bool InRange(this int @int, int maxRangeValue)
        => @int.InRange(0, maxRangeValue);
    
    public static bool InRange(this int @int, int minRangeValue, int maxRangeValue)
        => minRangeValue <= @int && @int <= maxRangeValue;
}