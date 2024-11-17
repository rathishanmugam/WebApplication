using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Linq.Expressions;
using WebApplication.Entities;

namespace WebApplication.Repositories;

public interface IUsersRepository
{
    Task<PagedUsersResult> GetUsers(string? term, string? sort, string? sortOrder, int page, int limit);
}
public class UsersRepository : IUsersRepository
{
    private readonly DataContext _userContext;
    public UsersRepository(DataContext userContext)
    {
        _userContext = userContext;
    }
    public async Task<PagedUsersResult> GetUsers(string? term, string? sort, string? sortOrder, int page, int limit)
    {
        IQueryable<Entities.User> users;
        if (string.IsNullOrWhiteSpace(term))
            users = _userContext.Users;
        else
        {
            term = term.Trim().ToLower();
            // filtering records with author or title
            users = _userContext.Users
                .Where(b => b.Title.ToLower().Contains(term) || b.FirstName.ToLower().Contains(term) || b.LastName.ToLower().Contains(term)
                || b.Email.ToLower().Contains(term));
        }
        // sorting
        Expression<Func<User, string>> keySelector = sort?.ToLower() switch
        {
            "title" => user => user.Title,
            "firstname" => user => user.FirstName,
            "lastname" => user => user.LastName,
            "email" => user => user.Email,
            _ => user => user.Id.ToString()
        };
        if (!string.IsNullOrWhiteSpace(sort))
        {
            if (sortOrder?.ToLower() == "desc")
            {
                users = _userContext.Users.OrderByDescending(keySelector);
            }
            else
            {
                users = _userContext.Users.OrderBy(keySelector);
            }
        }
        // apply pagination
        // totalCount=101 ,page=1,limit=10 (10 record per page)
        var totalCount = await _userContext.Users.CountAsync(); 
        var totalPages = (int)Math.Ceiling(totalCount / (double)limit);
         var HasNextPage = page * limit < totalCount;
         var HasPreviousPage = page > 1;  
        // page=1 , skip=(1-1)*10=0, take=10
        // page=2 , skip=(2-1)*10=10, take=10
        // page=3 , skip=(3-1)*10=20, take=10
        var pagedUsers = await users.Skip((page - 1) * limit).Take(limit).ToListAsync();

        var pagedUserData = new PagedUsersResult
        {
            Users = pagedUsers,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPage = HasNextPage,
            HasPreviousPage =HasPreviousPage
        };
        return pagedUserData;
    }

    
}