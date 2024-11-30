namespace Konyvtar.DataAcces.Model;

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public List<string> Authors { get; set; }

    public Book(string id, string title, Genre genre, List<string> authors)
    {
        Id = GenerateId();
        Title = title;
        Genre = genre;
        Authors = authors;
    }

    private static string GenerateId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Range(0, 12).Select(_ => chars[random.Next(chars.Length)]).ToArray());
    }
}
