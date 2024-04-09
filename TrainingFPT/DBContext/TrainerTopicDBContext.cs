using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingFPT.DBContext
{
    [Table("TrainerTopic")]
    public class TrainerTopicDBContext
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please choose Trainer")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please choose Topic")]
        public int TopicId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
