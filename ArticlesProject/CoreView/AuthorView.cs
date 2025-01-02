﻿using System.ComponentModel.DataAnnotations;

namespace ArticlesProject.CoreView
{
    public class AuthorView
    {
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Display(Name = "معرف المستخدم")]
        public required string UserId { get; set; }
        [Display(Name = "اسم المستخدم")]
        public required string UserName { get; set; }
        [Display(Name = "الاسم الكامل")]
        public required string FullName { get; set; }
        [Display(Name = "الصورة")]
        public IFormFile? ProfileImageUrl { get; set; }
        [Display(Name = "السيرة الذاتية")]
        public string? Bio { get; set; }
        [Display(Name = "فيسبوك")]
        public string? Facbook { get; set; }
        [Display(Name = "اسنتقرام ")]
        public string? Instragram { get; set; }
        [Display(Name = " أكس")]
        public string? Twitter { get; set; }
    }
}
