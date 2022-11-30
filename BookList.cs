public class BookList
{
    public List<Book> BookCollection { get; }

    public BookList()
    {
        using var reader = new StreamReader("books.json");
        
        var json = reader.ReadToEnd();
        
        BookCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>
            (json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
    }
}