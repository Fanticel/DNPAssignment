using Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepo;

public class EfcUserRepo : IUserRepo {
    private readonly AppContext _ctx;
    public EfcUserRepo(AppContext ctx) {
        _ctx = ctx;
    }
    public async Task<User> AddAsync(User user) {
        EntityEntry<User> entityEntry = await _ctx.userSet.AddAsync(user);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public async Task UpdateAsync(User user) {
        if (!(await _ctx.userSet.AnyAsync(u => u.Id == user.Id))) {
            throw new InvalidOperationException($"User with {user.Id} not found");
        }
        _ctx.userSet.Update(user);
        await _ctx.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        User? attempted = await _ctx.userSet.SingleOrDefaultAsync(u => u.Id == id);
        if (attempted is null) {
            throw new InvalidOperationException($"User with {id} not found");
        }
        _ctx.userSet.Remove(attempted);
        await _ctx.SaveChangesAsync();
    }
    public async Task<User> GetSingleAsync(int id) {
        User? attempted = await _ctx.userSet.SingleOrDefaultAsync(u => u.Id == id);
        if (attempted is null) {
            throw new InvalidOperationException($"User with {id} not found");
        }
        return attempted;
    }
    public IQueryable<User> GetMany() {
        return _ctx.userSet.AsQueryable();
    }
}