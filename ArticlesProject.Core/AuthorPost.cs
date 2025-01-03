using System.ComponentModel.DataAnnotations;

namespace ArticlesProject.Core
{
    public class AuthorPost
    {
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Display(Name = "معرف المستخدم")]
        public required string UserId { get; set; }
        [Display(Name = "اسم المستخدم")]
        public required string UserName { get; set; }
        [Display(Name = "الاسم الكامل")]
        public required string FullName { get; set; }

        [Display(Name = "الصنف")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Text)]
        public required string PostCategory { get; set; }
        [Display(Name = "العنوان")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Text)]
        public required string PostTitle { get; set; }
        [Display(Name = "الوصف")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.MultilineText)]
        public required string PostDescription { get; set; }
        [Display(Name = "الصورة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Upload)]
        public required string PostImageUrl { get; set; }
        [Display(Name = "تاريخ الاضافة")]
        public string AddedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
