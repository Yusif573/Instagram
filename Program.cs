using InstagramHW;
using InstagramHW.AdminPanel;
using InstagramHW.Notification;
using InstagramHW.Person;
using InstagramHW.UserPanel;
using System.Net.Mail;
using System.Net;

internal class Program
{
    static void selectFunction(ref int select, int maxSelect, int minSelect,ref bool enterSelected)
    {
        while (true)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
            {
                if (select == minSelect) { select = maxSelect; }
                else { select--; }
            }
            else if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
            {
                if (select == maxSelect) { select = minSelect; }
                else { select++; }
            }
            else if (consoleKeyInfo.Key == ConsoleKey.Enter) 
            {

                enterSelected = false;

            }
            return;
        }

    }
    static void showMenu(string[]menu, int select)
    {
        for (int i = 0; i < menu.Length; i++) 
        {
            if (select == i+1) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(menu[i]);
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
            Console.WriteLine(menu[i]);
        }
    }
    static void adminSignIn(List<AdminClass> admins, List<UserClass> users, List<InstagramPostClass> posts, List<NotificationClass> notifications)
    {
        Console.Clear();
        Console.Write("Enter email:");
        string? checkEmail = Console.ReadLine();
        Console.Write("Enter password:");
        string? checkPassword = Console.ReadLine();
        int select = 1;
        bool adminMenuEnter = true;
        foreach (var admin in admins)
        {
            if (admin.Email == checkEmail && admin.Password == checkPassword)
            {

                string? senderEmail = checkEmail;
                string senderPassword = "lvrl hqhv sqve ncbm";

                string? recipientEmail = "yusif.veliyev573@gmail.com";


                string smtpServer = "smtp.gmail.com";
                int port = 587; 
                SmtpClient client = new SmtpClient(smtpServer, port);
                client.EnableSsl = true; 

                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage message = new MailMessage(senderEmail, recipientEmail);
                message.Subject = "Verifying from instagram";
                Random random = new Random();
                int randomNumber = random.Next(1000);
                message.Body = $"{randomNumber}";
                Console.Clear();
                try
                {
                    client.Send(message);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    Console.Clear();
                    adminSignIn(admins, users, posts, notifications);
                }
                finally
                {
                    message.Dispose();
                    client.Dispose();
                }
                Console.Write("Enter verifying code:");
                int verCode = Convert.ToInt32(Console.ReadLine());
                if (randomNumber == verCode)
                {
                    Console.Clear();
                    Console.WriteLine("You have successfully entered)");
                    Thread.Sleep(2000);
                    string[] menuAfterLogIn = { "1 - Remove post", "2 - Add post", "3 - Notifications", "4 - Exit" };
                    while (true)
                    {
                        while (adminMenuEnter)
                        {
                            Console.Clear();
                            showMenu(menuAfterLogIn, select);
                            selectFunction(ref select, menuAfterLogIn.Length, 1, ref adminMenuEnter);
                            Console.Clear();
                        }
                        if (select == 1)
                        {
                            Console.Write("Enter id:");
                            int idToRemovePost = Convert.ToInt32(Console.ReadLine());
                            admin.removePost(idToRemovePost, posts, admin);
                        }
                        else if (select == 2)
                        {
                            admin.addPost(admin, posts);
                        }
                        else if (select == 3)
                        {
                            admin.showNotifications(notifications);
                        }
                        else
                        {
                            adminMenuSelect(admins, users, posts, notifications);
                            break;
                        }
                        adminMenuEnter = true;
                    }
                }
            }
        }
        Console.WriteLine("Incorrect information!");
        Thread.Sleep(2000);
        Console.Clear();
        Console.WriteLine("Press ESCAPE to return or ANY key to continue");
        ConsoleKeyInfo escapeKeyInfo = Console.ReadKey(true);
        if (escapeKeyInfo.Key == ConsoleKey.Escape) { adminMenuSelect(admins, users, posts, notifications); }
        else
        {
            adminSignIn(admins, users, posts, notifications);
        }

    }
    static void adminSignUp(List<AdminClass> admins,List<UserClass> users, List<InstagramPostClass> posts, List<NotificationClass> notifications)
    {
        Console.Clear();
        AdminClass newAdmin = new AdminClass();
        Console.Write("Enter age:");
        newAdmin.Age = Convert.ToUInt32(Console.ReadLine());
        Console.Write("Ente email:");
        newAdmin.Email = Console.ReadLine();
        Console.Write("Enter password:");
        newAdmin.Password = Console.ReadLine();
        foreach (AdminClass admin in admins)
        {
            if (newAdmin.Email.ToLower() == admin.Email.ToLower())
            {
                Console.WriteLine("Admin with this email already exist!");
                Thread.Sleep(2000);
                adminSignUp(admins,users,posts, notifications);
            }
        }
        while (true)
        {
            Console.Write("Enter username:");
            newAdmin.Username = Console.ReadLine();
            foreach (AdminClass admin in admins)
            {
                if (newAdmin.Username == admin.Username)
                {
                    Console.WriteLine("This username is used by other admin!");
                    Console.Write("Enter username:");
                    newAdmin.Username = Console.ReadLine();
                }
            }
            break;
        }
        admins.Add(newAdmin);
        Console.Clear();
        Console.WriteLine("You have successfully signed up)");
        Thread.Sleep(2000);
        Console.Clear();
        adminMenuSelect(admins,users, posts,notifications);


    }
    static void adminMenuSelect(List<AdminClass> admins,List<UserClass> users, List<InstagramPostClass> posts, List<NotificationClass> notifications)
    {
        string[] adminMenu = { "1 - Sign in","2 - Sign up", "3 - Exit" };
        int select = 1;
        bool adminMenuEnterSelected = true;
        while (adminMenuEnterSelected)
        {   
            showMenu(adminMenu, select);
            selectFunction(ref select, adminMenu.Length, 1,ref adminMenuEnterSelected);
            Console.Clear();
        }
        if (select == 1) 
        {
            adminSignIn(admins, users, posts,notifications);
        }
        else if (select == 2)
        {
            adminSignUp(admins, users, posts, notifications);
        }
        else { firstMenuAdminOrUserSelect(admins, users,posts,notifications); }
    }
    static void userSignIn(List<AdminClass> admins,List<UserClass> users,List<InstagramPostClass> posts,List<NotificationClass> notifications)
    {
        Console.Clear();
        Console.Write("Enter email:");
        string ?checkEmail = Console.ReadLine();
        Console.Write("Enter password:");
        string ?checkPassword = Console.ReadLine();
        int select = 1;
        bool userMenuEnter = true;
        foreach (var user in users)
        {
            if (user.Email == checkEmail && user.Password == checkPassword)
            {
                Console.Clear() ;
                Console.WriteLine("You have successfully entered)");
                Thread.Sleep(2000);
                List<string> menuAfterLogIn = new List<string>();
                foreach (var post in posts)
                {
                    menuAfterLogIn.Add(post.Content);
                }
                menuAfterLogIn.Add("Exit");

                while (userMenuEnter)
                {
                    Console.Clear();
                    showMenu(menuAfterLogIn.ToArray(), select);
                    selectFunction(ref select, menuAfterLogIn.Count, 1, ref userMenuEnter);
                    Console.Clear();
                    if (userMenuEnter == false)
                    {
                        while (true)
                        {
                            if (select != menuAfterLogIn.Count)
                            {
                                Console.Clear();
                                Console.WriteLine(posts[select - 1].showPost());
                                posts[select - 1].ViewCount++;
                                NotificationClass newViewNotification = new NotificationClass($"{user.Username} Viewed your post", DateTime.Now, user);
                                notifications.Add(newViewNotification);

                                //bool userViewCheck = true;
                                //foreach (NotificationClass notification in notifications)
                                //{
                                //    if (notification.FromUser == user)
                                //    {
                                //        userViewCheck = false;
                                //        break;
                                //    }
                                //    else { continue; }

                                //}
                                //if (userViewCheck)
                                //{
                                //    posts[select - 1].ViewCount++;
                                //    NotificationClass newViewNotification = new NotificationClass($"{user.Username} Viewed your post", DateTime.Now, user);
                                //    notifications.Add(newViewNotification);
                                //}
                                Console.WriteLine("Press L to like");
                                Console.WriteLine("Press ESCAPE to return");
                                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                                if (consoleKeyInfo.Key == ConsoleKey.Escape)
                                {
                                    Console.Clear();
                                    userMenuEnter = true;
                                    break;
                                }
                                else if (consoleKeyInfo.Key == ConsoleKey.L)
                                {
                                    Console.Clear();
                                    posts[select - 1].LikeCount++;
                                    NotificationClass newLikeNotification = new NotificationClass($"{user.Username} liked your post", DateTime.Now, user);
                                    notifications.Add(newLikeNotification);

                                    //bool userLikeCheck = true;
                                    //foreach (NotificationClass notification in notifications)
                                    //{
                                    //    if (notification.FromUser == user)
                                    //    {
                                    //        userLikeCheck = false;
                                    //        Console.WriteLine("You already liked this post");
                                    //        break;
                                    //    }
                                    //    else { continue; }

                                    //}
                                    //if (userLikeCheck)
                                    //{
                                    //    posts[select - 1].LikeCount++;
                                    //    NotificationClass newLikeNotification = new NotificationClass($"{user.Username} liked your post", DateTime.Now, user);
                                    //    notifications.Add(newLikeNotification);
                                    //}

                                }
                                else { continue; }
                            }
                            else
                            {
                                userMenuSelect(admins,users, posts, notifications);
                                return;
                            }
                        }
                    }
                }
            return;
            }
        }
        Console.WriteLine("Incorrect information!");
        Thread.Sleep(2000);
        Console.Clear();
        Console.WriteLine("Press ESCAPE to return or ANY key to continue");
        ConsoleKeyInfo escapeKeyInfo = Console.ReadKey(true);
        if (escapeKeyInfo.Key == ConsoleKey.Escape) { userMenuSelect(admins,users, posts, notifications); }
        else
        {
            userSignIn(admins, users, posts,notifications);
        }
    }
    static void userSignUp(List<AdminClass> admins, List<UserClass> users, List<InstagramPostClass> posts, List<NotificationClass> notifications)
    {
        Console.Clear();
        UserClass newUser = new UserClass();
        Console.Write("Ente name:");
        newUser.Name = Console.ReadLine();
        Console.Write("Enter surname:");
        newUser.Surname = Console.ReadLine();
        Console.Write("Enter age:");
        newUser.Age = Convert.ToUInt32(Console.ReadLine());
        Console.Write("Enter email:");
        newUser.Email = Console.ReadLine();
        Console.Write("Enter password:");
        newUser.Password = Console.ReadLine();
        foreach (UserClass user in users)
        {
            if(newUser.Email.ToLower() == user.Email.ToLower())
            {
                Console.WriteLine("User with this email already exist!");
                Thread.Sleep(2000);
                userSignUp(admins, users,posts, notifications);
            }
        }
        while (true)
        {
            Console.Write("Enter username:");
            newUser.Username = Console.ReadLine();
            foreach (UserClass user in users)
            {
                if (newUser.Username == user.Username)
                {
                    Console.WriteLine("This username is used by other user!");
                    Console.Write("Enter username:");
                    newUser.Username = Console.ReadLine();
                }
            }
            break;
        }
        users.Add(newUser);
        Console.Clear();
        Console.WriteLine("You have successfully signed up)");
        Thread.Sleep(2000);
        Console.Clear();
        userMenuSelect(admins,users, posts,notifications);


    }
    static void userMenuSelect(List<AdminClass> admins, List<UserClass> users, List<InstagramPostClass> posts,List<NotificationClass> notifications)
    {
        string[] userMenu = { "1 - Sign in", "2 - Sign up", "3 - Exit" };
        int select = 1;
        bool userMenuEnterSelected = true;
        while (userMenuEnterSelected)
        {

            showMenu(userMenu, select);
            selectFunction(ref select, userMenu.Length, 1,ref userMenuEnterSelected);
            Console.Clear();
        }
        if (select == 1) 
        {
            userSignIn(admins,users,posts,notifications);
        }
        else if (select == 2) 
        {
            userSignUp(admins, users,posts, notifications);
        }
        else 
        { 
            firstMenuAdminOrUserSelect(admins,users,posts, notifications);
        }
    }
    static void firstMenuAdminOrUserSelect(List<AdminClass> admins,List<UserClass> users,List<InstagramPostClass> posts, List<NotificationClass> notifications)
    {
        string[] menu = { "1 - Admin", "2 - User", "3 - Exit" };
        int select = 1;
        bool firstMenuEnterSelected = true;
        while (firstMenuEnterSelected)
        {
            showMenu(menu,select);
            selectFunction(ref select, menu.Length, 1,ref firstMenuEnterSelected);
            Console.Clear();
        }
        if (select == 1) adminMenuSelect(admins,users,posts,notifications);
        else if (select == 2) userMenuSelect(admins, users,posts,notifications);
        else return;

    }
    private static void Main(string[] args)
    {
        PersonClass._staticId = 1;
        InstagramPostClass._staticPostId = 1;
        AdminClass admin1 = new AdminClass("Yus123","yusif.veliyev573@gmail.com",16,"123321");
        List<AdminClass> admins = new List<AdminClass>();
        admins.Add(admin1);



        UserClass user1 = new UserClass("Yusif", "Veliyev", "Yus", 15, "y@", "123456");
        List<UserClass> users = new List<UserClass>();
        users.Add(user1);
        List<InstagramPostClass> instagramPosts = new List<InstagramPostClass>();
        InstagramPostClass post1 = new InstagramPostClass("Salam bu menim ilk contentimdir",admin1);
        instagramPosts.Add(post1);
        InstagramPostClass post2 = new InstagramPostClass("Bugun...", admin1);
        instagramPosts.Add(post2);

        NotificationClass notification1 = new NotificationClass("Hello",DateTime.Now,user1);
        List<NotificationClass> notifications = new List<NotificationClass>();

        Console.WriteLine("\t\t\tinstagram\t\t\t");
        firstMenuAdminOrUserSelect(admins,users,instagramPosts,notifications);
    }
}