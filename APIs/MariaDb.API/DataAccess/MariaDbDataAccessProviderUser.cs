using MariaDb.API.DataAccess.Interfaces;
using MariaDb.API.Models;

namespace MariaDb.API.DataAccess;

public class MariaDbDataAccessProviderUser : IMariaDbDataAccessProviderUser
{
    public Task<IEnumerable<UserModel>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> GetUser(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> AddUser(ProductModel user)
    {
        throw new NotImplementedException();
    }
}