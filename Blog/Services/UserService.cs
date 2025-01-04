using System.Security.Claims;
using System.Text;
using Blog.Data;
using Blog.Models;

namespace Blog.Services;

public class UserService
{
    private readonly BlogDbContext _dbContext;

    public UserService(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public UserModel Create(UserModel userModel)
    {
        var createdUser = new User
        {
            Name = userModel.Name,
            Email = userModel.Email,
            Password = userModel.Password,
            Description = userModel.Description,
            Photo = userModel.Photo,
        };

        _dbContext.Users.Add(createdUser);
        _dbContext.SaveChanges();

        userModel.Id = createdUser.Id;

        return userModel;
    }

    public UserModel Update(User updatedUser, UserModel userModel)
    {
        updatedUser.Name = userModel.Name;
        updatedUser.Email = userModel.Email;
        updatedUser.Password = userModel.Password;
        updatedUser.Description = userModel.Description;
        updatedUser.Photo = userModel.Photo;

        _dbContext.Users.Update(updatedUser);
        _dbContext.SaveChanges();

        return userModel;
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }

    public (string login, string password) GetUserLoginPassFromBasicAuth(HttpRequest request)
    {
        var userName = string.Empty;
        var userPass = string.Empty;
        var authHeader = request.Headers["Authorization"].ToString();
        
        if (!authHeader.StartsWith("Basic")) 
            return (userName, userPass);

        var encodedUserNamePass = authHeader.Replace("Basic ", "");
        var encoding = Encoding.GetEncoding("iso-8859-1");
        var namePassArray = encoding.GetString(Convert.FromBase64String(encodedUserNamePass))
            .Split(':');
        userName = namePassArray[0];
        userPass = namePassArray[1];
        return (userName, userPass);
    }

    public (ClaimsIdentity identity, int id)? GetIdentity(string email, string password)
    {
        var currentUser = GetUserByLogin(email);

        if (currentUser == null 
            || !VerifyHashedPassword(currentUser.Password, password)) 
            return null;

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
        };

        var claimsIdentity = new ClaimsIdentity(
            claims,
            "Token",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        return (claimsIdentity, currentUser.Id);
    }

    public User? GetUserByLogin(string email) =>
        _dbContext.Users.FirstOrDefault(x => x.Email == email);

    private bool VerifyHashedPassword(string password1, string password2) =>
        password1 == password2;
}