﻿namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Title {get; }
    public string Body {  get; }
    public User Owner { get; }
    
    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
    
    
}


