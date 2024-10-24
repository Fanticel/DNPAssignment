using Main;
using RepositoryContracts;

namespace InMemoryRepo;

public class UserInMemoryRepo : IUserRepo {
    private List<User> userList;

    public UserInMemoryRepo() {
        userList = new List<User>();
    }
    public Task<User> AddAsync(User user) {
        user.Id = userList.Any() ? userList.Max(u => u.Id) + 1 : 1;
        userList.Add(user);
        return Task.FromResult(user);
    }
    public Task UpdateAsync(User user) {
        User? userToUpdate = userList.SingleOrDefault(u => u.Id == user.Id);
        if (userToUpdate is null) {
            throw new InvalidOperationException($"User with ID {user.Id} not found :(");
        }
        userList.Remove(userToUpdate);
        userList.Add(user);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(int id) {
        User? userToDelete = userList.SingleOrDefault(u => u.Id == id);
        if (userToDelete is null) {
            throw new InvalidOperationException($"User with ID {id} not found :(");
        }
        userList.Remove(userToDelete);
        return Task.CompletedTask;
    }
    public Task<User> GetSingleAsync(int id) {
        User? userToGet = userList.SingleOrDefault(u => u.Id == id);
        if (userToGet is null) {
            throw new InvalidOperationException($"User with ID {id} not found :(");
        }
        return Task.FromResult(userToGet);
    }
    public IQueryable<User> GetMany() {
        return userList.AsQueryable();
    }
    public void CreateDummy() {
        AddAsync(new User("Alice123", "SecurePass1!"));
        AddAsync(new User("Bob_the_Builder", "FixIt2023#"));
        AddAsync(new User("Charlie88", "ChocoLover$"));
        AddAsync(new User("Daisy_Duke", "CountryLife@2024"));
        AddAsync(new User("a", "b"));
        AddAsync(new User("Eve_Electron", "TechSavvy&2022"));
        AddAsync(new User("Frankie_Fish", "SwimFast%"));
    } 
}