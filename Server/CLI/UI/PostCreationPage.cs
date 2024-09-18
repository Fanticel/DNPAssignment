using InMemoryRepo;
using Main;

namespace CLI.UI;

public class PostCreationPage {
    private PostInMemoryRepo _postInMemoryRepo;
    public PostCreationPage(PostInMemoryRepo postInMemoryRepo) {
        _postInMemoryRepo = postInMemoryRepo;
    }
    public void ShowCreation(int thisUserId) {
        while (true) {
            Console.Write("What should the title be? : ");
            string? potentialTitle = Console.ReadLine();
            Console.Write("\nContent : ");
            string? potentialBody = Console.ReadLine();
            if (potentialBody is not null && potentialBody.Any() && potentialTitle is not null && potentialTitle.Any()) {
                _postInMemoryRepo.AddAsync(new Post(thisUserId, potentialTitle, potentialBody));
                return;
            }
            Console.WriteLine("\nSomething went wrong\n\ntype 1 to try again\nanything else to leave");
            if(Console.ReadLine() != "1"){return;}
        }
    }
}