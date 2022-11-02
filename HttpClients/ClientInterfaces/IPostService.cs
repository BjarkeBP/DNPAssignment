using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    
    Task<ICollection<Post>> GetAsync(
        string? Title, 
        string? Body, 
        int? postId
    );
    
    Task<Post> GetByIdAsync(int id);
}