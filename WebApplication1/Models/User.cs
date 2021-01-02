namespace WebApplication1.Models
{
    public class User
    {
        public User(string sUser, int sId, string sPassword)
        {
            ID = sId;
            Name = sUser;
            Password = sPassword;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
