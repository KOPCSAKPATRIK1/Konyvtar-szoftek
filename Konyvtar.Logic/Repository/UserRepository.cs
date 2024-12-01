using Konyvtar.DataAcces.Interfaces.Repository;

namespace Konyvtar.Logic.Repository;

public class UserRepository : IUserRepository
{
    private readonly Dictionary<int, string> _users = new Dictionary<int, string>
    {
        { 1, "Teszt Felhasználó 1" },
        { 2, "Teszt Felhasználó 2" }
    };
    public string GetUserName(int userId)
    {
        if (_users.TryGetValue(userId, out var userName))
        {
            return userName;
        }
        return "Ismeretlen Felhasználó";
    }
}
