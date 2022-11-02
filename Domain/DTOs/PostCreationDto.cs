namespace Domain.DTOs;

public class PostCreationDto
{
    public string OwnerName { get; }
    public string Title { get; }
    
    public string Body { get; }

    public PostCreationDto(string OwnerName, string title, string body)
    {
        this.OwnerName = OwnerName;
        Title = title;
        Body = body;
    }
}