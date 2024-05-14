using System.ComponentModel.DataAnnotations;

namespace Praktika_1.Areas.Admin.ViewModels
{
    public class AdminLoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
