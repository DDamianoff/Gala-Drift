public class Book
{
    public string Title {get;set;}
    public int PageCount {get;set;}
    public string Status {get;set;}
    public DateTime PublishedDate {get;set;}
    public string[] Authors {get;set;}
    public string[] Categories {get;set;}
    
    public override string ToString() =>
        $"{Title} — " +
        $"{string.Join(' ', Authors)} " +
        $"{string.Join(' ', Categories.Select(c => $"#{c.Replace(' ', '_')}"))}";
}