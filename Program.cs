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