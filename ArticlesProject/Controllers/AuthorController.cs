using ArticlesProject.Core;
using ArticlesProject.CoreView;
using ArticlesProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IDataHelper<Author> _dataHelper;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAuthorizationService _authorizationService;
        private readonly FileHelper _fileHelper;
        private int _pageItem;

        public AuthorController(IDataHelper<Author> dataHelper, IWebHostEnvironment webHost, IAuthorizationService authorizationService)
        {
            _dataHelper = dataHelper;
            _webHost = webHost;
            _fileHelper = new FileHelper(_webHost);
            _pageItem = 10;
            _authorizationService = authorizationService;
        }

        [Authorize("Admin")]
        // GET: AuthorController
        public ActionResult Index(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(_dataHelper.GetAll().Take(_pageItem));
            }
            var data = _dataHelper.GetAll().Where(x => x.Id > id).Take(_pageItem);
            return View(data);
        }

        [Authorize("Admin")]
        public ActionResult Search(string SearchItem)
        {
            if (string.IsNullOrEmpty(SearchItem))
            {
                return View(nameof(Index), _dataHelper.GetAll());
            }

            return View(nameof(Index), _dataHelper.Search(SearchItem));

        }

        [Authorize]
        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = _dataHelper.Find(id);
            AuthorView authorView = new AuthorView
            {
                Id = author.Id,
                FullName = author.FullName,
                UserId = author.UserId,
                UserName = author.UserName,
                Bio = author.Bio,
                Instragram = author.Instragram,
                Facbook = author.Facbook,
                Twitter = author.Twitter,
            };
            return View(authorView);
        }

        [Authorize]
        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuthorView collection)
        {
            try
            {
                var author = new Author
                {
                    Id = collection.Id,
                    FullName = collection.FullName,
                    UserId = collection.UserId,
                    UserName = collection.UserName,
                    Bio = collection.Bio,
                    Instragram = collection.Instragram,
                    Facbook = collection.Facbook,
                    Twitter = collection.Twitter,
                    ProfileImageUrl = _fileHelper.UploadFile(collection.ProfileImageUrl, "Images")
                };

                _dataHelper.Update(id, author);

                var result = _authorizationService.AuthorizeAsync(User, "Admin");
                if (result.Result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return Redirect("/AdminIndex");
            }
            catch
            {
                return View();
            }
        }

        [Authorize("Admin")]
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
                Bio = author.Bio,
                Instragram = author.Instragram,
                Facbook = author.Facbook,
                Twitter = author.Twitter,
            };
            return View(authorView);
        }

        [Authorize("Admin")]
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
    }
}
