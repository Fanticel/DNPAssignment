using InMemoryRepo;
using Main;

namespace CLI.UI;

public class LoginPage {
    private UserInMemoryRepo userInMemoryRepo;
    public LoginPage(UserInMemoryRepo userInMemoryRepo) {
        this.userInMemoryRepo = userInMemoryRepo;
    }
    public int HandleLoginBegin() {
        while (true) {
            Console.WriteLine("Login : 1\n" +
                              "Register : 2");
            switch (Console.ReadLine()) {
                case "1": {
                    int resp = HandleLoginTotal();
                    if (resp == -1) {
                        Console.WriteLine("Something went wrong, telling you what would be a safety issue\n\n");
                    }
                    else {
                        return resp;
                    }
                    break;
                }
                case "2": {
                    int resp = HandleRegisterTotal();
                    if (resp == -1) {
                        Console.WriteLine("Something went wrong, telling you what would be a safety issue\n\n");
                    }
                    else {
                        return resp;
                    }
                    break;
                }
                default:
                    Console.WriteLine("Unrecognised command\nTryAgain");
                    break;
            }
        }
    }
    private int HandleLoginTotal() {
        User? tryUser;
        Console.Write("Username : ");
        string? username = Console.ReadLine();
        try {
            tryUser = userInMemoryRepo.GetMany().Single(d => d.UserName == username);
        }
        catch (InvalidOperationException e) {
            return -1;
        }
        Console.Write("Password : ");
        string? pass = Console.ReadLine();
        if (tryUser.UserPass == pass) {
            return tryUser.Id;
        }
        return -1;
    }
    private int HandleRegisterTotal() {
        Console.Write("Username : ");
        string? username = Console.ReadLine();
        if (userInMemoryRepo.GetMany().Any(d => d.UserName == username)) {
            return -1;
        }
        Console.Write("Password : ");
        string? pass = Console.ReadLine();
        return userInMemoryRepo.AddAsync(new User(username, pass)).Result.Id;
    }
}