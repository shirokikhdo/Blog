using System.Security.Claims;
using System.Text;
using Api.Data;
using Api.Models;

namespace Api.Services;

/// <summary>
/// Сервис для управления пользователями в блоге.
/// </summary>
/// <remarks>
/// Этот класс предоставляет методы для создания, обновления, удаления пользователей,
/// а также для получения информации о пользователях и их подписках.
/// </remarks>
public class UserService
{
    private readonly BlogDbContext _dbContext;
    private readonly NoSqlDataService _noSqlDataService;
    private readonly ImageService _imageService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserService"/>.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных для взаимодействия с пользователями.</param>
    /// <param name="noSqlDataService">Сервис для работы с NoSQL данными.</param>
    /// <param name="imageService">Сервис для обработки изображений пользователей.</param>
    public UserService(
        BlogDbContext dbContext, 
        NoSqlDataService noSqlDataService, 
        ImageService imageService)
    {
        _dbContext = dbContext;
        _noSqlDataService = noSqlDataService;
        _imageService = imageService;
    }

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="userModel">Модель пользователя, содержащая информацию о новом пользователе.</param>
    /// <returns>Созданная модель пользователя с установленным идентификатором.</returns>
    public UserModel Create(UserModel userModel)
    {
        var createdUser = new User
        {
            Name = userModel.Name,
            Email = userModel.Email,
            Password = userModel.Password,
            Description = userModel.Description,
            Photo = _imageService.GetPhoto(userModel.Photo),
        };

        _dbContext.Users.Add(createdUser);
        _dbContext.SaveChanges();

        userModel.Id = createdUser.Id;

        return userModel;
    }

    /// <summary>
    /// Обновляет информацию о существующем пользователе.
    /// </summary>
    /// <param name="updatedUser">Обновляемый пользователь.</param>
    /// <param name="userModel">Модель пользователя с новой информацией.</param>
    /// <returns>Обновленная модель пользователя.</returns>
    public UserModel Update(User updatedUser, UserModel userModel)
    {
        updatedUser.Name = userModel.Name;
        updatedUser.Email = userModel.Email;
        updatedUser.Password = userModel.Password;
        updatedUser.Description = userModel.Description;
        updatedUser.Photo = _imageService.GetPhoto(userModel.Photo);

        _dbContext.Users.Update(updatedUser);
        _dbContext.SaveChanges();

        return userModel;
    }

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="user">Пользователь, которого необходимо удалить.</param>
    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Извлекает логин и пароль пользователя из заголовка Basic Authentication.
    /// </summary>
    /// <param name="request">HTTP-запрос, содержащий заголовок авторизации.</param>
    /// <returns>Кортеж, содержащий логин и пароль пользователя.</returns>
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

    /// <summary>
    /// Получает идентификацию пользователя по электронной почте и паролю.
    /// </summary>
    /// <param name="email">Электронная почта пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>Кортеж, содержащий идентификацию и идентификатор пользователя, если пользователь найден; иначе - null.</returns>
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

    /// <summary>
    /// Получает пользователя по электронной почте.
    /// </summary>
    /// <param name="email">Электронная почта пользователя.</param>
    /// <returns>Объект пользователя или null, если пользователь не найден.</returns>
    public User? GetUserByLogin(string email) =>
        _dbContext.Users.FirstOrDefault(x => x.Email == email);

    /// <summary>
    /// Получает профиль пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Модель профиля пользователя или null, если пользователь не найден.</returns>
    public UserProfile? GetUserProfileById(int userId)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
        var userModel = user is null 
            ? null 
            : ToProfile(user);
        return userModel;
    }

    /// <summary>
    /// Подписывает одного пользователя на другого.
    /// </summary>
    /// <param name="from">Идентификатор подписчика.</param>
    /// <param name="to">Идентификатор подписываемого пользователя.</param>
    public void Subscribe(int from, int to) =>
        _noSqlDataService.SetUserSubscribes(from, to);

    /// <summary>
    /// Получает список пользователей по имени.
    /// </summary>
    /// <param name="name">Имя для поиска пользователей.</param>
    /// <returns>Список коротких моделей пользователей, соответствующих заданному имени.</returns>
    public List<UserShortModel> GetUsersByName(string name) =>
        _dbContext.Users
            .Where(x=>x.Name.ToLower().StartsWith(name.ToLower()))
            .Select(ToShortModel)
            .ToList();

    /// <summary>
    /// Преобразует объект <see cref="User"/> в объект <see cref="UserProfile"/>.
    /// </summary>
    /// <param name="user">Пользователь, который будет преобразован в профиль.</param>
    /// <returns>Объект <see cref="UserProfile"/>, представляющий профиль пользователя.</returns>
    public UserProfile ToProfile(User user)
    {
        var userSubs = _noSqlDataService.GetUserSubscribes(user.Id);
        var profile = new UserProfile 
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Description = user.Description,
            Photo = user.Photo,
            SubsCount = userSubs is null
                ? 0
                : userSubs.Users.Count
        };
        return profile;
    }

    /// <summary>
    /// Проверяет, совпадают ли два пароля.
    /// </summary>
    /// <param name="password1">Первый пароль для сравнения.</param>
    /// <param name="password2">Второй пароль для сравнения.</param>
    /// <returns>
    /// <c>true</c>, если пароли совпадают; в противном случае <c>false</c>.
    /// </returns>
    private bool VerifyHashedPassword(string password1, string password2) =>
        password1 == password2;

    /// <summary>
    /// Преобразует объект <see cref="User"/> в объект <see cref="UserShortModel"/>.
    /// </summary>
    /// <param name="user">Пользователь, который будет преобразован в короткую модель.</param>
    /// <returns>Объект <see cref="UserShortModel"/>, представляющий краткую информацию о пользователе.</returns>
    private UserShortModel ToShortModel(User user) =>
        new UserShortModel
        {
            Id = user.Id,
            Name = user.Name,
            Description = new string(user.Description?.Take(50).ToArray()),
            Photo = user.Photo
        };
}