using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;
public interface IUserDataService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserDetail(Guid userId);
}