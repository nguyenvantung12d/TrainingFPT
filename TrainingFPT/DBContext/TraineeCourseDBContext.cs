using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingFPT.DBContext
{
    [Table("TraineeCourse")]
    public class TraineeCourseDBContext
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter UserId, please")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Enter CourseId, please")]
        public int CourseId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
