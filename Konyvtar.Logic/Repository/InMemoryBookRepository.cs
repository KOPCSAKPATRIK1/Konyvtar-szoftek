using Konyvtar.DataAcces.Interfaces.Repository;
using Konyvtar.DataAcces.Model;

namespace Konyvtar.Logic.Repository;

public class InMemoryBookRepository : IRepository<Book>
{
    private readonly List<Book> _books = new List<Book>();  // Könyvek tárolása memóriában.

    // Könyv hozzáadása a listához.
    public void Add(Book book)
    {
        _books.Add(book);
    }

    // Könyv frissítése az azonosító alapján, ha már létezik.
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

    // Könyv törlése az azonosító alapján.
    public void Delete(string id)
    {
        var book = Get(id);
        if (book != null)
        {
            _books.Remove(book);
        }
    }
    // Egy adott könyv lekérése az azonosító alapján.
    public Book Get(string id) => _books.FirstOrDefault(b => b.Id == id);

    // Az összes könyv listázása.
    public List<Book> GetAll()
    {
        return new List<Book>(_books);// Új listát ad vissza a könyvek másolataként.
    }
}
