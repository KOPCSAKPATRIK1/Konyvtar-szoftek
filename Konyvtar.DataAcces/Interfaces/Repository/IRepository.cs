using Konyvtar.DataAcces.Model;

namespace Konyvtar.DataAcces.Interfaces.Repository;

public interface IRepository<T>
{
    void Add(T item);
    void Update(T item);
    void Delete(string id);
    T Get(string id);
    List<T> GetAll();
}
