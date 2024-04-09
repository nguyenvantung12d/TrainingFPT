using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TrainingFPT.DBContext
{
    [Table("Courses")]
    public class CourseDBContext
    {
        [Key]
        public int Id { get; set; }
        
        [Column("CategoryId"), Required]
        public int CategoryId {  get; set; }

        [Column("NameCourse", TypeName ="nvarchar(60)"), Required]
        public string NameCourse { get; set; }

        [Column("Description", TypeName="nvarchar(500)"), AllowNull]
        public string? Description { get; set; }

        [Column("Image", TypeName ="Varchar(MAX)")]
        public string? Image {  get; set; }

        [Column("LikeCourse", TypeName ="integer"), AllowNull]
        public int? LikeCourse { get; set;}

        [Column("StarCourse", TypeName = "integer"), AllowNull]
        public int? StarCourse { get; set; }

        [Column("Status", TypeName = "Varchar(20)"), Required]
        public string Status { get; set; }

        [AllowNull]
        public DateTime? CreatedAt { get; set; }

        [AllowNull]
        public DateTime? UpdatedAt { get; set; }

        [AllowNull]
        public DateTime? DeletedAt { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }
        [NotMapped]
        public IFormFile? FileImage { get; set; }
    }
}
