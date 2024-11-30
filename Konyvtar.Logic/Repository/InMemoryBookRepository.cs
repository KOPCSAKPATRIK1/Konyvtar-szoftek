using Konyvtar.DataAcces.Interfaces.Repository;
using Konyvtar.DataAcces.Model;

namespace Konyvtar.Logic.Repository;

public class InMemoryBookRepository : IRepository<Book>
{
    private readonly List<Book> _books = new List<Book>();

    public void Add(Book book)
    {
        _books.Add(book);
    }

    public void Update(Book book)
    {
        var existingBook = Get(book.Id);
        if (existingBook != null)
        {
            existingBook.Title = book.Title;
            existingBook.Genre = book.Genre;
            existingBook.Authors = book.Authors;
        }
    }

    public void Delete(string id)
    {
        var book = Get(id);
        if (book != null)
        {
            _books.Remove(book);
        }
    }

    public Book Get(string id) => _books.FirstOrDefault(b => b.Id == id);

    public List<Book> GetAll()
    {
        return new List<Book>(_books);
    }
}
