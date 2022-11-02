using StudyShared.Models;

namespace StudyShared.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
    }
}