using Domain.DTOs;
using Domain.LogicInterfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;
    
    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    
    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto)
    {
        try
        {
            Post created = await postLogic.CreateAsync(dto);
            return Created($"/Posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Post>>>
        GetAsync([FromQuery] string? title, [FromQuery] string? body,
        [FromQuery] int? postID)
    {
        try
        {
            SearchPostParametersDto parameters = new(title, body, postID);
            var Posts = await postLogic.GetAsync(parameters);
            return Ok(Posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{postID:int}"), AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Post>>>
        GetById([FromRoute] int? postID)
    {
        try
        {
            Console.WriteLine(postID);
            SearchPostParametersDto parameters = new(null, null ,postID);
            var Posts = await postLogic.GetAsync(parameters);
            return Ok(Posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}