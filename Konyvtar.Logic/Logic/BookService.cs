using Konyvtar.DataAcces.Interfaces.Logic;
using Konyvtar.DataAcces.Interfaces.Repository;
using Konyvtar.DataAcces.Model;

namespace Konyvtar.Logic.Logic;

public class BookService : IBookService
{
    private readonly IRepository<Book> _repository; // Könyv adattároló.
    private readonly IUserRepository _userRepository; // Felhasználó adattároló.

    // Konstruktor, amely inicializálja a könyv- és felhasználó-adattárolót.
    public BookService(IRepository<Book> repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    // Új könyv hozzáadása ellenőrzéssel.
    public void AddBook(string id, string title, Genre genre, List<string> authors)
    {
        // Ellenőrzi, hogy létezik-e már könyv az adott azonosítóval.
        if (_repository.GetAll().Any(b => b.Id == id))
        {
            throw new ArgumentException($"Már létezik könyv ezzel az azonosítóval: {id}");
        }

        // Ellenőrzi, hogy a könyv címe nem üres és legalább 4 karakter hosszú.
        if (string.IsNullOrWhiteSpace(title) || title.Length < 4)
        {
            throw new ArgumentException("A könyv címe nem lehet üres, és legalább 4 karakter hosszúnak kell lennie.");
        }

        // Új könyv létrehozása és hozzáadása a tárolóhoz.
        var book = new Book(id, title, genre, authors);
        _repository.Add(book);

        LogOperation("Létrehozás", book.Title);
    }

    // Az összes könyv visszaadása listaként.
    public List<Book> GetAllBooks()
    {
        return _repository.GetAll();
    }

    // Egy szerző nevének átírása minden könyvben.
    public void RenameAuthorInBooks(string oldName, string newName)
    {
        var books = _repository.GetAll();
        foreach (var book in books) 
        {
            for (int i = 0; i < book.Authors.Count; i++)
            {
                // Ha megtalálja a régi nevet, kicseréli az újra.
                if (book.Authors[i] == oldName)
                {
                    book.Authors[i] = newName;
                }
            }
            // Frissíti a könyvet az új szerzőnévvel.
            _repository.Update(book);
            LogOperation("Szerző átnevezése", book.Title);
        }
    }

    // Egy könyv műfajának frissítése az azonosító alapján.
    public void UpdateBookGenre(string id, Genre genre)
    {
        var book = _repository.Get(id);
        if (book != null)
        {
            // Műfaj módosítása és könyv frissítése.
            book.Genre = genre;
            _repository.Update(book);
            LogOperation("Műfaj frissítése", book.Title);
        }
    }

    // Művelet naplózása konzolra.
    private void LogOperation(string operationType, string bookTitle)
    {
        // Bejelentkezett felhasználó azonosítójának lekérése a Singletonon keresztül.
        int userId = UserSession.Instance.GetUserId();
        // Felhasználó nevének lekérése az azonosító alapján.
        string userName = _userRepository.GetUserName(userId);

        // Üzenet kiírása a konzolra.
        Console.WriteLine($"[{operationType}] Könyv címe: {bookTitle}, Felhasználó: {userName}");
    }
}
