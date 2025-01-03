using ArticlesProject.Core;
using ArticlesProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ArticlesProject.Pages
{
    [Authorize]
    public class AdminIndexModel : PageModel
    {
        private readonly IDataHelper<AuthorPost> _dataHelper;
        public AdminIndexModel(IDataHelper<AuthorPost> dataHelper)
        {
            _dataHelper = dataHelper;
        }
        public int allPosts { get; set; }
        public int postLastMonth { get; set; }
        public int postInThisYear { get; set; }
        public void OnGet()
        {
            var datem = DateTime.Now.AddMonths(-1);
            var datey = DateTime.Now.AddYears(-1);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            allPosts = _dataHelper.GetUserById(userId).Count();
            postLastMonth = _dataHelper.GetUserById(userId).Where(x => DateTime.Parse(x.AddedDate) >= datem).Count();
            postInThisYear = _dataHelper.GetUserById(userId).Where(x => DateTime.Parse(x.AddedDate) >= datey).Count();
        }
    }
}
