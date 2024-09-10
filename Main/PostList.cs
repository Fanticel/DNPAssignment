namespace Main;

public class PostList {
    public List<Post> Posts;
    public PostList(List<Post> ReadyList) {
        Posts = ReadyList;
    }
    public PostList() {
        Posts = new List<Post>();
        GenerateSampleData();
    }
    private void GenerateSampleData() {
        Posts.Add(new Post(new User("Sample", "1234"), "AITA for losing my partner’s dog while I was supposed to be watching him?",
            "I (27F) have been dating my boyfriend (29M) for about a year, and recently I moved in with him. He has a dog, Max, who is a 4-year-old golden retriever. I adore Max, but I’ve never really had pets growing up, so I’m still getting used to the responsibility.\n\nLast weekend, my boyfriend had to leave town for work, so I was in charge of watching Max for the first time. He gave me a rundown of Max’s routine, and I thought everything was going fine.\n\nOne afternoon, I took Max to the nearby dog park to let him burn off some energy. We’d been there for about 30 minutes when I got a phone call from a friend I hadn’t spoken to in a while. I stepped away from Max (he was playing with other dogs, so I thought he was fine), but I got wrapped up in the conversation and wasn’t paying close attention. After I hung up and went to check on him, Max was gone.\n\nI panicked and searched the park, but I couldn’t find him anywhere. I immediately called my boyfriend to let him know what happened, and he was furious. He drove back home right away (cutting his work trip short), and we searched the area for hours, eventually finding Max about a mile away, running through a neighbor’s yard. Thankfully, he was fine, but my boyfriend hasn’t been able to forgive me.\n\nHe says I was irresponsible and that I shouldn’t have been on the phone while watching Max. I feel terrible and have apologized multiple times, but he’s still upset and says he doesn’t trust me to watch Max again.\n\nI know I messed up, but it was an honest mistake, and Max wasn’t hurt. I don’t think I deserve to be punished this harshly for it, but my boyfriend and even some of my friends say I was careless."));
        Posts.Add(new Post(new User("Wanderer123", "P@ssword123"), 
    "AITA for not letting my friend bring her dog to my wedding?", 
    "So, I (30F) am getting married in two months, and one of my closest friends (29F) asked if she could bring her dog to the wedding. She has a small, very well-behaved dog that she takes everywhere with her. I totally understand that her dog is a huge part of her life, but my fiancé and I are having a formal indoor wedding, and we’ve made it clear that it’s a no-pet event. When I politely told her this, she got upset and said that her dog is basically family and that it would be unfair to leave him out of such an important day. I feel like I’m in a tough spot because I don’t want to hurt her feelings, but I also don’t think it’s appropriate to have pets at a wedding. My fiancé supports my decision, but some of our mutual friends think I’m being too rigid about it. AITA for sticking to my no-pets rule?"));
        Posts.Add(new Post(new User("BikerDude88", "B!k3rP@ss"), 
            "Looking to get into mountain biking—where should I start?", 
            "Hey everyone! I've been a road cyclist for a couple of years now, and I'm thinking about transitioning to mountain biking.  I have no idea what kind of bike I should buy, what trails are good for beginners, or even what gear I need for off-roading. Can anyone give me some pointers or recommend a good beginner-friendly trail in the Pacific Northwest? Thanks for any advice you can give!"));
        Posts.Add(new Post(new User("UserName", "UserPassword"), "PostTitle", "PostBody"));
    }
    public void PostPost(User user, string Title, string Body) {
        Posts.Add(new Post(user, Title, Body));
    }

    public override string ToString() {
        string re = "";
        foreach (Post post in Posts) {
            re += post + "\n____________________________________________\n";
        }
        return re;
    }
    public int GetLength() {
        return Posts.Count;
    }
}