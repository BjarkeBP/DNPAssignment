namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? Title { get; }
    
    public string? Body { get; }
    public int? PostID { get; }

    public SearchPostParametersDto(string? title, string? body, int? postID)
    {
        Title = title;
        Body = body;
        PostID = postID;
    }
}