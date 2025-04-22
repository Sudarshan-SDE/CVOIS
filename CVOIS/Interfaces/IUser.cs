namespace CVOIS.Interfaces
{
    public interface IUser
    {
        bool CheckOldPassword(string userId, string oldPassword);
        bool UpdateUserPassword(string userId, string newPassword);
    }
}
