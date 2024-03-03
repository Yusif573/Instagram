using System.ComponentModel;
using System.Xml.Linq;
namespace InstagramHW.Person;

public abstract class PersonClass
{
    static public int _staticId {  get; set; }
    public int _id {  get; set; }
    public string? Username { get; set; }

    protected string? _email;
    public string? Email
    {
        get { return _email; }
        set
        {
            if (value.Contains("@")) _email = value;
            else
            {
                Console.WriteLine("Incorrect information!");
                Console.Write("Enter email:");
                Email = Console.ReadLine();
            }
        }
    }
    private uint? _age;
    public uint? Age
    {
        get { return _age; }
        set 
        { 
            if (value is uint)
            {
                _age = value;
            }
            else
            {
                Console.WriteLine("Must be a number!");
                Console.WriteLine("Enter age:");
                Age = Convert.ToUInt32(Console.ReadLine());
            }
        }
    }
     
    protected string? _password;
    public string? Password
    {
        get { return _password; }
        set
        {
            if (value.Length < 6)
            {
                Console.WriteLine("At least must be 6 symbols!");
                Console.Write("Enter password:");
                Password = Console.ReadLine();
            } 
            else
            {
                _password = value;
            }
        }
    }

    protected PersonClass()
    {
        _id = _staticId++;
    }
    protected PersonClass(string username,string email,uint age,string password)
        :this()
    {
        Username = username;
        Email = email;
        Age = age;
        Password = password;
    }
    public override string ToString()
    {
        return $"Id:{_id}\nUsername:{Username}\nEmail:{_email}\nPassword:{_password}\nAge:{Age}";
    }

}

