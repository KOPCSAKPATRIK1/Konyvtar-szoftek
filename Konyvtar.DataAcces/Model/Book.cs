namespace Konyvtar.DataAcces.Model;

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public List<string> Authors { get; set; }

    public Book(string id, string title, Genre genre, List<string> authors)
    {
        Id = id;
        Title = title;
        Genre = genre;
        Authors = authors;
    }
}
