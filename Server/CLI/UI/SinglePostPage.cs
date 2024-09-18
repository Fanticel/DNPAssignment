using InMemoryRepo;
using Main;

namespace CLI.UI;

public class SinglePostPage {
    private CommentInMemoryRepo _commentInMemoryRepo;
    private UserInMemoryRepo _userInMemoryRepo;
    private int _localUser;
    private bool running;
    public SinglePostPage(CommentInMemoryRepo commentInMemoryRepo, UserInMemoryRepo userInMemoryRepo, int localUser) {
        _commentInMemoryRepo = commentInMemoryRepo;
        _userInMemoryRepo = userInMemoryRepo;
        _localUser = localUser;
    }
    public void ShowFullPost(Post viewedPost) {
        running = true;
        while (running) {
            Console.WriteLine($"Op : {_userInMemoryRepo.GetSingleAsync(viewedPost.posterId).Result.UserName}\n" +
                              $"\tPost title : \t{viewedPost.PostTitle}\n" +
                              $"\t\tPost body : \t{viewedPost.PostBody}\n\n" +
                              $"Up : {viewedPost.Likes} | Down : {viewedPost.Dislikes}\n" +
                              $"Comments: ");
            foreach (int x in viewedPost.CommentIds) {
                Comment c = _commentInMemoryRepo.GetSingleAsync(x).Result;
                Console.WriteLine($"\t\t{_userInMemoryRepo.GetSingleAsync(c.getPoster()).Result.UserName} says " +
                                  $"\"{c.CommentBody}\"");
            }
            Console.WriteLine("Like post : 1\n" +
                              "Dislike post : 2\n" +
                              "Leave a comment : 3\n" +
                              "Go back to homepage : 4");
            HandleResponse(Console.ReadLine(), viewedPost);
        }
    }
    public void HandleResponse(string? response, Post viewedPost) {
        switch (response) {
            case "1":
                viewedPost.Likes++;
                break;
            case "2":
                viewedPost.Dislikes++;
                break;
            case "3": {
                Console.Write("What should the comment say? : ");
                string? ans = Console.ReadLine();
                if (ans is not null && ans.Any()) {
                    viewedPost.PostComment(_commentInMemoryRepo.AddAsync(new Comment(_localUser, ans)).Result.Id);
                }
                else {
                    Console.WriteLine("Can't post an empty comment, press enter to continue");
                    Console.ReadLine();
                }
                break;
            }
            case "4":
                running = false;
                break;
        }
    }
}