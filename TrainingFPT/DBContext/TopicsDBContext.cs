using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TrainingFPT.Validations;

namespace TrainingFPT.DBContext
{
    public class TopicsDBContext
    {
        [Key]
        public int Id { get; set; }

        [Column("CouresId"), Required]
        public int CouresId { get; set; }

        [Column("NameTopic", TypeName = "nvarchar(200)"), Required]
        public string NameTopic { get; set; }

        [Column("Description", TypeName ="nvarchar(MAX)"), AllowNull]
        public string? Description { get; set; }

        [Column("Status", TypeName = "Varchar(20)"), Required]
        public string Status { get; set; }

        [Column("DocumentTopic", TypeName = "Varchar(MAX)")]
        public string? DocumentTopic { get; set; }

        [Column("Video", TypeName = "Varchar(MAX)"), AllowNull]
        public string? Video { get; set; }

        [Column("Audio", TypeName = "Varchar(MAX)")]
        public string? Audio { get; set; }
        public int? LikeTopic { get; set; }
        public int? StarTopic { get; set; }
        [AllowNull]
        public DateTime? CreatedAt { get; set; }

        [AllowNull]
        public DateTime? UpdatedAt { get; set; }

        [AllowNull]
        public DateTime? DeletedAt { get; set; }
        [NotMapped]
        public string? CourseName { get; set; }
        [NotMapped]
        public IFormFile? FileVideo { get; set; }
        [NotMapped]
        public IFormFile? FileAudio { get; set; }
        [NotMapped]
        public IFormFile? FileDocument { get; set; }   


    }
}
