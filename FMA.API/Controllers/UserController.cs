using FMA.BLL.Services.Interfaces;
using FMA.DAL.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace FMA.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController :  ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service) 
        {
        _service = service;
        }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUser()
    {
        var users = await _service.GetAllUser();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User?>> GetUserById(int id)
    {
        var user = await _service.GetUserById(id);
        return user;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        return await _service.CreateUser(user);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> UpdateUser(User user)
    {
        return await _service.UpdateUser(user);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {
        return await _service.DeleteUser(id);
    }

}