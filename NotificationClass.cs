using InstagramHW.UserPanel;

namespace InstagramHW.Notification;

public class NotificationClass
{
    //id,Text,DateTime,FromUser(bu hansi user terefinden bu bildirishin geldiyidir)
    static public int _notificationStaticId {  get; set; }
    public int _notificationId { get; set; }
    public string? Text {  get; set; }
    public DateTime NotificationDate { get; set; }
    public UserClass FromUser { get; set; }
    public NotificationClass()
    {
        _notificationId = _notificationStaticId++;
    }
    public NotificationClass(string text,DateTime notificationDate,UserClass user)
        :this()
    {
        Text = text;
        NotificationDate = notificationDate;
        FromUser = user;
    }
    public string showNotification()
    {
        return $"Id:{_notificationId}\nDate time:{NotificationDate}\nUser:{FromUser.Username}";
    }
}
