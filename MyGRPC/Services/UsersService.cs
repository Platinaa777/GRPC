using Grpc.Core;
using MyGRPC.Data;

namespace MyGRPC.Services;

public class UsersService : Users.UsersBase
{
    private readonly DataContext _db;

    public UsersService(DataContext db)
    {
        _db = db;
    }
    public override Task<AllUsersResponse> GetAllUsers(AllUsersRequest request, ServerCallContext context)
    {
        var users = _db.Users.Select(x => new UserResponse() { Id = x.Id, Name = x.Name }).ToList();
    
        var list = new AllUsersResponse();
        list.ListUsers.AddRange(users);
    
        return Task.FromResult(list);
    }


    public override Task<UserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var user = new User() {Name = request.Name};
        var temp =_db.Users.Add(user);
        _db.SaveChanges();
        var response = new UserResponse() { Id = temp.Entity.Id, Name = temp.Entity.Name };
        return Task.FromResult(response);
    }

    public override Task<AllUsersResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var user = _db.Users.FirstOrDefault(x => x.Id == request.Id);
        if (user == null)
        {
            return null;
        }

        _db.Users.Remove(user);
        _db.SaveChanges();
        var users = _db.Users.Select(x => new UserResponse() { Id = x.Id, Name = x.Name }).ToList();
    
        var list = new AllUsersResponse();
        list.ListUsers.AddRange(users);
    
        return Task.FromResult(list);
    }

    public override Task<UserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var user = _db.Users.FirstOrDefault(x => x.Id == request.Id);
        if (user == null)
        {
            return null;
        }

        user.Name = request.Name;
        _db.SaveChanges();
        return Task.FromResult(new UserResponse() {Id = user.Id, Name = user.Name});
    }
}
