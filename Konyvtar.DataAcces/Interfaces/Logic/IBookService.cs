using Konyvtar.DataAcces.Model;

namespace Konyvtar.DataAcces.Interfaces.Logic
{
    public interface IBookService
    {
        void AddBook(string id, string title,Genre genre, List<string> authors);
        void UpdateBookGenre(string id, Genre genre);
        void RenameAuthorInBooks(string oldName, string newName);
        List<Book> GetAllBooks();
    }
}
