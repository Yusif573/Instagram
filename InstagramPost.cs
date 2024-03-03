using InstagramHW.AdminPanel;

namespace InstagramHW;

public class InstagramPostClass
{
    static public int _staticPostId {  get; set; }
    public int _postId {  get; set; }
    public AdminClass Admin {  get; set; }
    public string? Content {  get; set; }
    public DateTime CreationDateTime {  get; set; }
    public uint LikeCount {  get; set; }
    public uint ViewCount {  get; set; }
    public InstagramPostClass()
    {
        _postId = _staticPostId++;
    }
    public InstagramPostClass(string content,AdminClass admin)
        :this()
    {
        Admin = admin;
        Content = content;
        CreationDateTime = DateTime.Now;
    }
    public string showPost()
    {
        return $"Id:{_postId}\nAdmin:{Admin.Username}\n{Content}\n{CreationDateTime}\nLikes:{LikeCount}\tViews:{ViewCount}";
    }
}
