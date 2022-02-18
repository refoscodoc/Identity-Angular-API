using MariaDb.API.Models;

namespace MariaDb.API.DataAccess.Interfaces;

public interface IMariaDbDataAccessProviderUser
{
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel> GetUser(Guid Id);
    Task<UserModel> AddUser(ProductModel user);
}