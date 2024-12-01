using Konyvtar.DataAcces.Interfaces.Repository;

namespace Konyvtar.Logic.Repository;

public class UserRepository : IUserRepository
{
    public string GetUserName(int userId)
    {
        return "Teszt Felhasználó";
    }
}
