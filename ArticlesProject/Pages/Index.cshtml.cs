using ArticlesProject.Core;
using ArticlesProject.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArticlesProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<Core.Category> _dataHelperForCategory;
        private readonly IDataHelper<AuthorPost> _dataHelperForAuthorPost;
        private int _numberOfItems;

        public IndexModel(ILogger<IndexModel> logger, IDataHelper<Core.Category> dataHelperForCategory, IDataHelper<AuthorPost> dataHelperForAuthorPost)
        {
            _logger = logger;
            _dataHelperForCategory = dataHelperForCategory;
            _dataHelperForAuthorPost = dataHelperForAuthorPost;
            _numberOfItems = 5;
        }

        public IEnumerable<Core.Category>? categoryList { get; set; }
        public IEnumerable<Core.AuthorPost>? postList { get; set; }


        public void OnGet()
        {
            GetAllCategory();
            GetAllPost();
        }

        private void GetAllCategory()
        {
            categoryList = _dataHelperForCategory.GetAll();
        }

        private void GetAllPost()
        {
            postList = _dataHelperForAuthorPost.GetAll().Take(_numberOfItems);
        }
    }
}
