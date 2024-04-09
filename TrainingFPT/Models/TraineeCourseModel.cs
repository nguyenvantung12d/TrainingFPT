using System.ComponentModel.DataAnnotations;

namespace TrainingFPT.Models
{
    public class TraineeCourseModel
    {
        public List<TraineeCourseDetail> TraineeCourseDetailLists { get; set; }
    }
    public class TraineeCourseDetail
    {
        public int Id {  get; set; }    
        [Required(ErrorMessage = "Choose Course, please")]
        public int Courseid { get; set; }
        [Required(ErrorMessage = "Choose Trainee, please")]

        public int UserId { get; set; }

        public DateTime? Createdat { get; set; }

        public DateTime? Updatedat { get; set; }

        public DateTime? Deletedat { get; set; }
        public string? CourseName { get; set; }
        public string? TraineeName { get; set; }

    }
}
