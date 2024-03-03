using InstagramHW.Notification;
using InstagramHW.Person;


namespace InstagramHW.AdminPanel;

public class AdminClass:PersonClass
{
    //id,username,email,password,Posts,Notifications
    //List<InstagramPostClass> posts = new List<InstagramPostClass>();
    //List<NotificationClass> notifications = new List<NotificationClass>();
    //static public int _staticAdminId { get; set; }
    //public int _adminId { get; set; }
    public AdminClass()
        :base()
    {
        
    }
    public AdminClass(string username,string email,uint age,string password)
        :base(username,email,age,password)
    {
        
    }
    public void addPost(AdminClass thisAdmin,List<InstagramPostClass> posts)
    {
        Console.Write("Content:");
        string content = Console.ReadLine();
        InstagramPostClass newPost = new InstagramPostClass(content,thisAdmin);
        posts.Add(newPost);
    }
    public void removePost(int idToRemove,List<InstagramPostClass> posts,AdminClass admin)
    {
        foreach (InstagramPostClass post in posts)
        {
            if(idToRemove == post._postId && post.Admin.Email == admin.Email)
            {
                posts.Remove(post);
                return;
            }
            else if (post.Admin.Email != admin.Email) 
            {
                Console.WriteLine("You can not remove other user post!");
            }
        }
        Console.WriteLine("Incorrect information");
    }
    public void showNotifications(List<NotificationClass> notifications)
    {
        foreach(NotificationClass notification in notifications)
        {
            Console.WriteLine(notification.showNotification());
        }
    }
}
