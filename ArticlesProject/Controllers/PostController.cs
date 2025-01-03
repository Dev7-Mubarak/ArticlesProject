using ArticlesProject.Core;
using ArticlesProject.CoreView;
using ArticlesProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArticlesProject.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IDataHelper<AuthorPost> _dataHelper;
        private readonly IDataHelper<Author> _dataHelperAuthor;
        private readonly IDataHelper<Category> _dataHelperCategory;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private Task<AuthorizationResult> _result;
        private readonly FileHelper _fileHelper;
        private int _pageItem;
        private string _userId;

        public PostController(IDataHelper<AuthorPost> dataHelper, IWebHostEnvironment webHost, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IDataHelper<Author> dataHelperAuthor, IDataHelper<Category> dataHelperCategory)
        {
            _dataHelper = dataHelper;
            _webHost = webHost;
            _fileHelper = new FileHelper(_webHost);
            _pageItem = 10;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _signInManager = signInManager;
            _dataHelperAuthor = dataHelperAuthor;
            _dataHelperCategory = dataHelperCategory;
        }

        // GET: AuthorController
        public ActionResult Index(int? id)
        {
            SetUser();
            // Admin
            if (_result.Result.Succeeded)
            {
                if (id == 0 || id == null)
                {
                    return View(_dataHelper.GetAll().Take(_pageItem));
                }
                var data = _dataHelper.GetAll().Where(x => x.Id > id).Take(_pageItem);
                return View(data);
            }
            else
            {
                if (id == 0 || id == null)
                {
                    return View(_dataHelper.GetUserById(_userId).Take(_pageItem));
                }
                var data = _dataHelper.GetUserById(_userId).Where(x => x.Id > id).Take(_pageItem);
                return View(data);
            }

            // User
        }

        public ActionResult Search(string SearchItem)
        {
            if (string.IsNullOrEmpty(SearchItem))
            {
                return View(nameof(Index), _dataHelper.GetAll());
            }

            return View(nameof(Index), _dataHelper.Search(SearchItem));

        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorPostView collection)
        {
            SetUser();
            try
            {
                var post = new AuthorPost
                {
                    AddedDate = DateTime.Now.ToString(),
                    Author = collection.Author,
                    Category = collection.Category,
                    PostCategory = collection.PostDescription,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = _userId,
                    PostImageUrl = _fileHelper.UploadFile(collection.PostImageUrl, "Images"),
                    UserName = _dataHelperAuthor.GetAll()
                    .Where(x => x.UserId == _userId)
                    .Select(x => x.UserName)
                    .FirstOrDefault(),
                    FullName = _dataHelperAuthor.GetAll()
                    .Where(x => x.UserId == _userId)
                    .Select(x => x.FullName)
                    .FirstOrDefault(),
                    AuthorId = _dataHelperAuthor.GetAll()
                    .Where(x => x.UserId == _userId)
                    .Select(x => x.Id)
                    .FirstOrDefault(),
                    CategoryId = _dataHelperCategory.GetAll()
                    .Where(x => x.Name == collection.PostCategory)
                    .Select(x => x.Id)
                    .FirstOrDefault(),
                };
                var result = _dataHelper.Add(post);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Detailes(int id)
        {
            SetUser();
            return View(_dataHelper.Find(id));
        }
        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var authorPost = _dataHelper.Find(id);
            AuthorPostView authorPostView = new AuthorPostView
            {
                AddedDate = authorPost.AddedDate.ToString(),
                Author = authorPost.Author,
                Category = authorPost.Category,
                PostCategory = authorPost.PostDescription,
                PostDescription = authorPost.PostDescription,
                PostTitle = authorPost.PostTitle,
                UserId = _userId,
                UserName = authorPost.UserName,
                FullName = authorPost.FullName,
                AuthorId = authorPost.AuthorId,
                CategoryId = authorPost.CategoryId,
                Id = authorPost.Id
            };
            return View(authorPostView);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuthorPostView collection)
        {
            SetUser();
            try
            {
                var post = new AuthorPost
                {
                    AddedDate = DateTime.Now.ToString(),
                    Author = collection.Author,
                    Category = collection.Category,
                    PostCategory = collection.PostDescription,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = _userId,
                    PostImageUrl = _fileHelper.UploadFile(collection.PostImageUrl, "Images"),
                    UserName = _dataHelperAuthor.GetAll()
                    .Where(x => x.UserId == _userId)
                    .Select(x => x.UserName)
                    .FirstOrDefault(),
                    FullName = _dataHelperAuthor.GetAll()
                    .Where(x => x.UserId == _userId)
                    .Select(x => x.FullName)
                    .FirstOrDefault(),
                    AuthorId = _dataHelperAuthor.GetAll()
                    .Where(x => x.UserId == _userId)
                    .Select(x => x.Id)
                    .FirstOrDefault(),
                    CategoryId = _dataHelperCategory.GetAll()
                    .Where(x => x.Name == collection.PostCategory)
                    .Select(x => x.Id)
                    .FirstOrDefault(),
                    Id = collection.Id
                };
                var result = _dataHelper.Update(id, post);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = _dataHelper.Find(id);
            AuthorView authorView = new AuthorView
            {
                Id = author.Id,
                FullName = author.FullName,
                UserId = author.UserId,
                UserName = author.UserName,
                //Bio = author.Bio,
                //Instragram = author.Instragram,
                //Facbook = author.Facbook,
                //Twitter = author.Twitter,
            };
            return View(authorView);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author collection)
        {
            try
            {
                _dataHelper.Delete(id);
                string filePath = "~/Images/" + collection.ProfileImageUrl;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }


        }

        private void SetUser()
        {
            _result = _authorizationService.AuthorizeAsync(User, "Admin");
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
