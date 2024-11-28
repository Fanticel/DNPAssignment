namespace Main;

public class User {
    public int Id { get; set;}
    public string UserName { get; set; }
    public string UserPass { get; set; }
    private User(){} //for EFC
    public User(string userName, string userPass) {
        UserName = userName;
        UserPass = userPass;
    }
}