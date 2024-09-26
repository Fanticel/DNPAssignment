using RepositoryContracts;

namespace CLI.UI;

public class ProfilePage {
    private IUserRepo _userInMemoryRepo;
    public ProfilePage(IUserRepo userInMemoryRepo) {
        _userInMemoryRepo = userInMemoryRepo;
    }
    public void ShowProfile(int thisUserId) {
        Console.WriteLine($"Username:\t {_userInMemoryRepo.GetSingleAsync(thisUserId).Result.UserName}\n" +
                          $"Password:\t {_userInMemoryRepo.GetSingleAsync(thisUserId).Result.UserPass}\n" +
                          $"Id: \t      {_userInMemoryRepo.GetSingleAsync(thisUserId).Result.Id}");
        Console.WriteLine("To go back press enter");
        Console.ReadLine();
    }
}