using InstagramHW.Person;

namespace InstagramHW.UserPanel;

public class UserClass:PersonClass
{
    //static public int _staticId { get; set; }
    //public int _id {  get; set; }
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (value.Length < 3)
            {
                Console.WriteLine("Name lenght must be at least 3 symbols!");
                Console.Write("Enter name:");
                Name = Console.ReadLine();
            }
            else { _name = value; }
        }
    }
    private string _surname;
    public string Surname
    {
        get { return _surname; }
        set
        {
            if (value.Length < 5) 
            {
                Console.WriteLine("Surname lenght must be at least 5 symbols!");
                Console.Write("Enter surname:");
                Surname = Console.ReadLine();
            }
            else
            {
                _surname = value;
            }
        }
    }
    public UserClass()
        :base()
    {
        
    }
    public UserClass(string name, string surname, string username, uint age, string email, string password)
        : base(username,email,age,password)
    {
        Name = name;
        Surname = surname;
    }
    public override string ToString()
    {
        return $"Name:{_name}\nSurname:{_surname}\n"+base.ToString();
    }
}
