// See https://aka.ms/new-console-template for more information

using CLI.UI;
using InMemoryRepo;

CommentInMemoryRepo commentInMemoryRepo = new CommentInMemoryRepo();
PostInMemoryRepo postInMemoryRepo = new PostInMemoryRepo();
UserInMemoryRepo userInMemoryRepo = new UserInMemoryRepo();
userInMemoryRepo.CreateDummy();
commentInMemoryRepo.CreateDummy();
postInMemoryRepo.CreateDummy();

CliApp cliApp = new CliApp(postInMemoryRepo, commentInMemoryRepo, userInMemoryRepo);
cliApp.StartAsync();