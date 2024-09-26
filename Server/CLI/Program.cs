// See https://aka.ms/new-console-template for more information

using CLI.UI;
using FileMemoryRepo;
using RepositoryContracts;

IUserRepo userInMemoryRepo = new UserInFileRepo();
ICommentRepo commentInMemoryRepo = new CommentInFileRepo();
IPostRepo postInMemoryRepo = new PostInFileRepo();

CliApp cliApp = new CliApp(postInMemoryRepo, commentInMemoryRepo, userInMemoryRepo);
cliApp.StartAsync();