using InMemoryRepo;
using Main;

namespace CLI.UI;

public class CliApp {
    private PostInMemoryRepo _postInMemoryRepo;
    private CommentInMemoryRepo _commentInMemoryRepo;
    private UserInMemoryRepo _userInMemoryRepo;
    private LoginPage _loginPage;
    private SinglePostPage _singlePostPage;
    private ProfilePage _profilePage;
    private PostCreationPage _postCreationPage;
    private int thisUserId;
    private bool running;
    public CliApp(PostInMemoryRepo postInMemoryRepo, CommentInMemoryRepo commentInMemoryRepo, UserInMemoryRepo userInMemoryRepo) {
        _postInMemoryRepo = postInMemoryRepo;
        _commentInMemoryRepo = commentInMemoryRepo;
        _userInMemoryRepo = userInMemoryRepo;
        _loginPage = new LoginPage(_userInMemoryRepo);
        _profilePage = new ProfilePage(_userInMemoryRepo);
        _postCreationPage = new PostCreationPage(_postInMemoryRepo);
        running = true;
    }
    public void StartAsync() {
        thisUserId = _loginPage.HandleLoginBegin();
        _singlePostPage = new SinglePostPage(_commentInMemoryRepo, _userInMemoryRepo, thisUserId);
        while (running) {
            Console.WriteLine(GenerateHomePage());
            Console.WriteLine("Inspect post : 1;x (x being the id of the post)\n" +
                              "Post a new post : 2\n" +
                              "View my profile : 3\n" +
                              "Close the app : 4");
            HandleResponse(Console.ReadLine());
        }
        Console.WriteLine("See you later ;)");
    }
    private string GenerateHomePage() {
        string ans = "";
        ans += ("| Home page |\n_____________");
        foreach (Post p in _postInMemoryRepo.GetMany()) {
            ans += ($"\n| Id : {p.PostId} |\n| {_userInMemoryRepo.GetSingleAsync(p.posterId).Result.UserName} |\n| {p.PostTitle} |\n| {p.Likes} : {p.Dislikes} |\n_________");
        }
        return ans;
    }
    private void HandleResponse(string resp) {
        string[] ans = resp.Split(";");
        switch (ans[0]) {
            case "1":
                _singlePostPage.ShowFullPost(_postInMemoryRepo.GetSingleAsync(int.Parse(ans[1])).Result);
                break;
            case "2":
                _postCreationPage.ShowCreation(thisUserId);
                break;
            case "3":
                _profilePage.ShowProfile(thisUserId);
                break;
            case "4":
                running = false;
                break;
            default:
                Console.WriteLine("Unrecognised command, try again");
                break;
        }
    }
}