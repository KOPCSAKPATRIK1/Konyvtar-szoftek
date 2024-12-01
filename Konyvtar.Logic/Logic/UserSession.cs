namespace Konyvtar.Logic.Logic;

public class UserSession
{
    private static readonly UserSession _instance = new UserSession();
    private int _userId = 1;

    private UserSession() { }

    public static UserSession Instance
    {
        get { return _instance; }
    }

    public int GetUserId()
    {
        return _userId;
    }
    public void SetUserId(int userId)
    {
        _userId = userId;
    }
}
