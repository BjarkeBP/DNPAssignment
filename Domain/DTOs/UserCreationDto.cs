namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get; }
    public string PassWord { get; }
    
    public UserCreationDto(string userName, string passWord)
    {
        UserName = userName;
        PassWord = passWord;
    }
}