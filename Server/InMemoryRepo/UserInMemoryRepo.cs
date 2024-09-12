using Main;
using RepositoryContracts;

namespace InMemoryRepo;

public class UserInMemoryRepo : IUserRepo {
    private List<User> userList;
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
}