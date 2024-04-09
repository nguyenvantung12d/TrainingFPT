using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingFPT.DBContext
{
    [Table("Users")]
    public class UsersDBContext
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter role, please")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Enter username, please")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter password, please")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Enter email, please")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter phone, please")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Address, please")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter BirthDay, please")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Enter Gender, please")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Enter ExtraCode, please")]
        public string ExtraCode { get; set; }
        public string? Avatar { get; set; }
        public string? Education { get; set; }
        public string? ProgramingLang { get; set; }
        public int ToeicScore { get; set; }
        public string? Skills { get; set; }
        public string? IPClient { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastLogout { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
