namespace Main;

public class User {
    private int UserId;
    public string UserName { get; set; }
    public string UserPass { get; set; }

    public User(string userName, string userPass) {
        UserId = 1;
        UserName = userName;
        UserPass = userPass;
    }
}