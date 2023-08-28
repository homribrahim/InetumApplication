using System.ComponentModel.DataAnnotations;

namespace InetumAPP.Models
{
    public class User
    {
        [Key]

        public int id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set;}

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }
    }
}
