namespace WebApplication.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Users;
using WebApplication.Repositories;
using WebApplication.Services;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
 private readonly IUsersRepository _userRepo;

    public UsersController(IUsersRepository userRepo,
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
        _userRepo = userRepo;
    }
    [HttpGet]
     public async Task<IActionResult> GetUsers(string? term, string? sort,string? sortOrder, int page = 1, int limit = 2)
        {
            var userResult = await _userRepo.GetUsers(term, sort,sortOrder, page, limit);

            // Add pagination headers to the response
            // Response.Headers.Add("X-Total-Count", userResult.TotalCount.ToString());
            // Response.Headers.Add("X-Total-Pages", userResult.TotalPages.ToString());
            return Ok(userResult);
        }
    // public IActionResult GetAll()
    // {
    //     var users = _userService.GetAll();
    //     return Ok(users);
    // }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(CreateRequest model)
    {
        _userService.Create(model);
        return Ok(new { message = "User created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}