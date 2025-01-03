using System.ComponentModel.DataAnnotations;

namespace ArticlesProject.Core
{
    public class Category
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "أسم الصنف")]
        [MaxLength(50, ErrorMessage = "أعلى قمية للأدخال هي 50 حرف")]
        [MinLength(2, ErrorMessage = "أدنئ قمية للأدخال هي حرفان")]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        public virtual List<AuthorPost> AuthorPosts { get; set; }
    }
}
