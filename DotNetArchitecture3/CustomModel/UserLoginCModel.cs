using System.ComponentModel.DataAnnotations;

namespace DotNetArchitecture3.CustomModel
{
    public class UserLoginCModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
