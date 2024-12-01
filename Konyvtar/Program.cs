using Konyvtar.DataAcces.Interfaces.Logic;
using Konyvtar.DataAcces.Interfaces.Repository;
using Konyvtar.DataAcces.Model;
using Konyvtar.Logic.Logic;
using Konyvtar.Logic.Repository;

IRepository<Book> repository = new InMemoryBookRepository();
IUserRepository userRepository = new UserRepository();
IBookService bookService = new BookService(repository, userRepository);

repository.Add(new Book("000000000001", "Az alkimista", Genre.Fantasy, new List<string> { "Paulo Coelho" }));
repository.Add(new Book("000000000002", "A kis herceg", Genre.Thriller, new List<string> { "Antoine de Saint-Exupéry" }));
repository.Add(new Book("000000000003", "Harry Potter és a bölcsek köve", Genre.Fantasy, new List<string> { "J.K. Rowling" }));
repository.Add(new Book("000000000004", "A Gyűrűk Ura", Genre.Fantasy, new List<string> { "J.R.R. Tolkien" }));

while (true)
{
    Console.WriteLine("1. Új könyv felvétele");
    Console.WriteLine("2. Könyv műfajának módosítása");
    Console.WriteLine("3. Szerző nevének átírása");
    Console.WriteLine("4. Minden könyv listázása");
    Console.WriteLine("0. Kilépés");
    var choice = Console.ReadLine();

    if (choice == "0") break;

    switch (choice)
    {

        case "1":
            try
            {
                Console.Write("Azonosító (12 karakter hosszú): ");
                var id = Console.ReadLine();

                Console.Write("Cím (legalább 4 karakter hosszú): ");
                var title = Console.ReadLine();

                Console.Write("Szerzők (vesszővel elválasztva): ");
                var authors = Console.ReadLine().Split(',').ToList();

                Console.WriteLine("Válassz műfajt az alábbiak közül:");
                int num = 0;
                foreach (var genreName in Enum.GetNames(typeof(Genre)))
                {
                    Console.WriteLine($"{num} {genreName}");
                    num++;
                }

                Console.Write("Műfaj: ");
                var genreInput = Console.ReadLine();
                if (Enum.TryParse(genreInput, true, out Genre genre))
                {
                    bookService.AddBook(id, title, genre, authors);
                    Console.WriteLine("Könyv sikeresen hozzáadva.");
                }
                else
                {
                    Console.WriteLine("Érvénytelen műfaj.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
            break;

        case "2":
            
           var books = bookService.GetAllBooks();
            if (books.Any())
            {
                Console.WriteLine("A könyvek listája:");
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.Id}, Cím: {book.Title}, Műfaj: {book.Genre}, Szerzők: {string.Join(", ", book.Authors)}");
                }
            }
            try
            {

                Console.Write("Könyv azonosítója: ");
                var id = Console.ReadLine();

                Console.WriteLine("Válassz új műfajt az alábbiak közül:");
                int num = 0;
                foreach (var genreName in Enum.GetNames(typeof(Genre)))
                {
                    Console.WriteLine($"{num} {genreName}");
                    num++;
                }

                Console.Write("Új műfaj: ");
                var genreInput = Console.ReadLine();
                if (Enum.TryParse(genreInput, true, out Genre genre))
                {
                    bookService.UpdateBookGenre(id, genre);
                    Console.WriteLine("Könyv műfaja frissítve.");
                }
                else
                {
                    Console.WriteLine("Érvénytelen műfaj.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
            break;

        case "3":
            try
            {
                Console.Write("Régi szerző neve: ");
                var oldName = Console.ReadLine();

                Console.Write("Új szerző neve: ");
                var newName = Console.ReadLine();

                bookService.RenameAuthorInBooks(oldName, newName);
                Console.WriteLine("A szerző neve minden könyvben módosítva lett.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
            break;

        case "4":
            books = bookService.GetAllBooks();
            if (books.Any())
            {
                Console.WriteLine("A könyvek listája:");
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.Id}, Cím: {book.Title}, Műfaj: {book.Genre}, Szerzők: {string.Join(", ", book.Authors)}");
                }
            }
            else
            {
                Console.WriteLine("Nincsenek elérhető könyvek.");
            }
            break;

        default:
            Console.WriteLine("Érvénytelen választás.");
            break;
    }
}

