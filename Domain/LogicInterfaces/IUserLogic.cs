using Domain.DTOs;
using Domain.Models;

namespace Domain.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userCreationDto);
    Task<User> ValidateUser(string username, string password);
}