var books = new BookList().BookCollection;

books
    .Where(b => 
        b.Categories
            .Select(s => s.Normalized())
            .Contains("Python".Normalized()))
    .Print("python books:");   

books
    .Where(b => 
        b.Categories
            .Select(t => t.Normalized())
            .Contains("Java".Normalized()))
    .OrderBy(b => b.Title)
    .Print("Java books ordered:");

books
    .Where(b => b.PageCount > 450)
    .OrderByDescending(b => b.PageCount)
    .Print("Books with more than 450 pages DescOrder.");
// PD:I'm skipping normalize strings from now on
books
    .Where(c => c.Categories.Contains("Java"))
    .OrderByDescending(b => b.PublishedDate)
    .Take(3)
    .Print("Three first most recent java books");

books
    .Where(b => b.PageCount > 400)
    .OrderBy(b => b.PageCount)
    .Take(4)
    .Skip(2)
    .Print("3th and 4th books with most pages:");

books
    .Count(p => p.PageCount.InRange(200,500))
    .Print("Amount of books with PageCount between 200 - 500");

books
    .Min(b => b.PublishedDate)
    .Print("Oldest publish date");

books
    .MaxBy(b => b.PageCount)
    .Print("Book with most pages");

books
    .Where(b => b.PageCount.InRange(200,500))
    .Sum(b => b.PageCount)
    .Print("Sum of all page counts between 200 and 500");

books
    .Where(b => b.PublishedDate.Year > 2015)
    .Aggregate("", (titles, next) =>
        {
            titles += string.IsNullOrEmpty(titles) 
                ? $"{next.Title[..12]}..." 
                : $" — {next.Title[..12]}...";
            return titles;
        })
    .Print("All book titles published after 2015");

books
    .Where(b => b.PageCount != 0)
    .Average(b => b.PageCount)
    .Print("Average page count");

books
    .GroupBy(b => b.Title[0] , b => b)
    .Print("Books grouped by titles with First char as Key");

books
    .ToLookup(b => b.Title[0] , b => b)
    .Print("Same as before but with lookUp");

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