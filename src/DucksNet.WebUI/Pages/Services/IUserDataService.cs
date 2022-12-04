using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface IUserDataService
{
    Task CreateUser(CreateUserModel userCreateModel);
    Task DeleteUser(string userId);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserDetail(Guid userId);
    Task UpdateUser(string userId, UpdateUserModel updateUserModel);
}