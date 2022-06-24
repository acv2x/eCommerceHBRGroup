namespace eCommerceAPI.Models
{
    public class User
    {
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }

        public User(int id, string? email, string? password, string? fullname)
        {
            ID = id;
            Email = email;
            FullName = fullname;
            Password = password;
        }
    }
}