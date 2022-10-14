using System.ComponentModel.DataAnnotations;

namespace DotNetArchitecture3.Models
{
    public class UserMaster
    {
        [Key]
        public int UserId { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string AadharCard { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
    }
}
