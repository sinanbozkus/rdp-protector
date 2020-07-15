using System.ComponentModel.DataAnnotations;

namespace RdpProtector.Models
{
    public class AccountViewModel
    {
        [Required]
        public string SecurityKey { get; set; }
    }
}
