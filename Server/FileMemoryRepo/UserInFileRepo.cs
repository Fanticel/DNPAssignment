using System.Text.Json;
using Main;
using RepositoryContracts;

namespace FileMemoryRepo;

public class UserInFileRepo : IUserRepo {
    private readonly string _filePath = "UserFile.json";
    public UserInFileRepo() {
        if (!File.Exists(_filePath)) {
            File.WriteAllText(_filePath, "[]");
            CreateDummy();
        }
    }
    public async Task<User> AddAsync(User user) {
        List<User> listUser = JsonSerializer.Deserialize<List<User>>(await File.ReadAllTextAsync(_filePath))!;
        user.Id = listUser.Any() ? listUser.Max(d => d.Id) + 1 : 1;
        listUser.Add(user);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(listUser));
        return user;
    }
    public async Task UpdateAsync(User user) {
        List<User> listUser = JsonSerializer.Deserialize<List<User>>(await File.ReadAllTextAsync(_filePath))!;
        User? userInQuestion = listUser.SingleOrDefault(d => d.Id == user.Id);
        if (userInQuestion is null) {
            throw new InvalidOperationException($"User with ID {user.Id} not found :(");
        }
        listUser.Remove(userInQuestion);
        listUser.Add(user);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(listUser));
    }
    public async Task DeleteAsync(int id) {
        List<User> listUser = JsonSerializer.Deserialize<List<User>>(await File.ReadAllTextAsync(_filePath))!;
        User? userInQuestion = listUser.SingleOrDefault(d => d.Id == id);
        if (userInQuestion is null) {
            throw new InvalidOperationException($"User with ID {id} not found :(");
        }
        listUser.Remove(userInQuestion);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(listUser));
    }
    public async Task<User> GetSingleAsync(int id) {
        List<User> listUser = JsonSerializer.Deserialize<List<User>>(await File.ReadAllTextAsync(_filePath))!;
        User? userInQuestion = listUser.SingleOrDefault(d => d.Id == id);
        if (userInQuestion is null) {
            throw new InvalidOperationException($"User with ID {id} not found :(");
        }
        return userInQuestion;
    }
    public IQueryable<User> GetMany() {
        return JsonSerializer.Deserialize<List<User>>(File.ReadAllTextAsync(_filePath).Result)!.AsQueryable();
    }
    public async void CreateDummy() {
        await AddAsync(new User("Alice123", "SecurePass1!"));
        await AddAsync(new User("Bob_the_Builder", "FixIt2023#"));
        await AddAsync(new User("Charlie88", "ChocoLover$"));
        await AddAsync(new User("Daisy_Duke", "CountryLife@2024"));
        await AddAsync(new User("a", "b"));
        await AddAsync(new User("Eve_Electron", "TechSavvy&2022"));
        await AddAsync(new User("Frankie_Fish", "SwimFast%"));
    } 
}