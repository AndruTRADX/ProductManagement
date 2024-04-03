using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Context;
using ProductManagement.Models;
using ProductManagement.DTOs;

namespace ProductManagement.Services;

public class UserServices(UserContext userContext, IPasswordHasher<User> passwordHasher) : IUserService
{
    private readonly UserContext _dbContext = userContext;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task<IEnumerable<UserDTO>> GetAll(int page, int pageSize)
    {
        var users = await _dbContext.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UserDTO
            {
                UserID = u.UserId,
                Name = u.Name,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role
            })
            .ToListAsync();

        return users;
    }

    public async Task<UserDTO?> Get(Guid id)
    {
        var user = await _dbContext.Users
            .Where(u => u.UserId == id)
            .Select(u => new UserDTO
            {
                UserID = u.UserId,
                Name = u.Name,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role
            })
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task Save(User user)
    {
        user.Password = HashPassword(user, user.Password);
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Guid id, User user)
    {
        var currentUser = await _dbContext.Users.FindAsync(id);

        if (currentUser != null)
        {
            currentUser.Name = user.Name;
            currentUser.UserName = user.UserName;
            currentUser.Email = user.Email;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                currentUser.Password = HashPassword(user, user.Password);
            }

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var currentUser = await _dbContext.Users.FindAsync(id);

        if (currentUser != null)
        {
            _dbContext.Users.Remove(currentUser);
            await _dbContext.SaveChangesAsync();
        }
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }
}
public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAll(int page, int pageSize);
    Task<UserDTO?> Get(Guid id);
    Task Save(User user);
    Task Update(Guid id, User user);
    Task Delete(Guid id);
}