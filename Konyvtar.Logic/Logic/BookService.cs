using Konyvtar.DataAcces.Interfaces.Logic;
using Konyvtar.DataAcces.Interfaces.Repository;
using Konyvtar.DataAcces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konyvtar.Logic.Logic
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }
        public void AddBook(string id, string title, Genre genre, List<string> authors)
        {
            var book = new Book(id, title, genre, authors);
            _repository.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return _repository.GetAll();
        }

        public void RenameAuthorInBooks(string oldName, string newName)
        {
            var books = _repository.GetAll();
            foreach (var book in books) 
            {
                for (int i = 0; i < book.Authors.Count; i++)
                {
                    if (book.Authors[i] == oldName)
                    {
                        book.Authors[i] = newName;
                    }
                }
                _repository.Update(book);
            }
        }

        public void UpdateBookGenre(string id, Genre genre)
        {
            var book = _repository.Get(id);
            if (book != null)
            {
                book.Genre = genre;
                _repository.Update(book);
            }
        }
    }
}
