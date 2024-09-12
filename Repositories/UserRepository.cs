namespace Tidjma.Repository;
using Microsoft.EntityFrameworkCore;
using Tidjma.Models;
using Tidjma.Helpers;
using Tidjma.Contracts;
using Tidjma.Data;

/*
 *
 * Fix Error Handling here
 * 
 * */
public class UserRepository
{
    private readonly TidjmaDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;
    private readonly BlogMapper _mapper;

    public UserRepository(
            BlogMapper mapper,
            TidjmaDbContext DbC,
            ILogger<UserRepository> logger
           )
    {
        _dbContext = DbC;
        _logger = logger;
        _mapper = mapper;
    }

    public UserDTO? Get(int id) 
    { 
        var user = _dbContext.Users.Find(id);

        return user is not null ? _mapper.UserToUserDTO(user) : null;
    }

    public IEnumerable<UserDTO?> List() 
    { 
        return _dbContext
            .Users
            .ToList()
            .Select(e => _mapper.UserToUserDTO(e));
    }

    public bool Create(CreateUserDTO newUser)
    {
        var user = _mapper.CreateUserDTOToUser(newUser);
        _dbContext.Users.Add(user); 

        return _dbContext.SaveChanges() > 0 ? true : false;
    }

    public bool Update(UpdateUserDTO updatedUser, int id)
    {
        _dbContext.Update(_mapper.UpdateUserDTOToUser(updatedUser));
        return _dbContext.SaveChanges() > 0 ? true : false;
    }

    public bool Delete(int id)
    {
        var user =_dbContext.Users.Find(id);
        if (user is not null)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }

        return false;
    }
}
