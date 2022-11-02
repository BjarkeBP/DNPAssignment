using System.Text.RegularExpressions;
using Domain.DaoInterfaces;
using Domain.DTOs;
using Domain.LogicInterfaces;
using Domain.Models;

namespace Domain.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? exising = await userDao.GetByUsernameAsync(dto.UserName);
        if (exising != null)
        {
            throw new Exception("Username already taken");
        }

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            PassWord = dto.PassWord
        };

        User created = await userDao.CreateAsync(toCreate);

        return created;
    }


    private static void ValidateData(UserCreationDto dto)
    {
        string userName = dto.UserName;

        if (userName.Length < 3)
        {
            throw new Exception("Username must be at least 3 characters!");
        }

        if (userName.Length > 15)
        {
            throw new Exception("Username must be less than 16 characters!");
        }

        string passWord = dto.PassWord;

        if (passWord.Length < 3)
        {
            throw new Exception("Password must be at least 6 characters!");
        }

        var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

        if (regexItem.IsMatch(passWord))
        {
            throw new Exception("password must contain special characters!");
        }
        
        if (!passWord.Any(char.IsUpper))
        {
            throw new Exception("password must contain upper case characters!");
        }

    }
    
    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = userDao.GetByUsernameAsync(username).Result;
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.PassWord.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    
    
    
    
}