using Main;

Console.WriteLine("I got ahead of myself, ._.");
//
// bool running = true;
// Console.Write("Login: ");
// string name = Console.ReadLine();
// Console.Write("\nPassword: ");
// string pass = Console.ReadLine();
// User thisUser = new User(name, pass);
// PostList postList = new PostList();
// while (running) {
//     Console.WriteLine("1 -> Get a list of Posts");
//     Console.WriteLine("2;x -> Get a post number x, along with comments and body");
//     Console.WriteLine("0 -> Leave the application");
//     string ans = Console.ReadLine();
//     switch (ans.Split(";")[0]) {
//         case "1":
//             Console.WriteLine(postList);
//             break;
//         case "2":
//             int req = int.Parse(ans.Split(";")[1]);
//             if (req > postList.GetLength()) {
//                 Console.WriteLine("No such post exists");
//             }
//             else {
//                 bool inPost = true;
//                 while (inPost) {
//                     Console.WriteLine(postList.Posts[req].FullToString());
//                     Console.WriteLine("1 -> Like the post \n2 -> Dislike the post \n3;x -> Leave a comment x\n4 -> Go back to Master");
//                     string[] subAns = Console.ReadLine().Split(";");
//                     switch (subAns[0]) {
//                         case "1" :
//                             postList.Posts[req].Like();
//                             break;
//                         case "2" :
//                             postList.Posts[req].Dislike();
//                             break;
//                         case "3" :
//                             postList.Posts[req].PostComment(thisUser, subAns[1]);
//                             break;
//                         case "4" :
//                             inPost = false;
//                             break;
//                     }
//                 }
//             }
//             break;
//         case "0":
//             Console.WriteLine("Bye");
//             running = false;
//             break;
//     }
// }