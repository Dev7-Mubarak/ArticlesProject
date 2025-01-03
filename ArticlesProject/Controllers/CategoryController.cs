using ArticlesProject.Core;
using ArticlesProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesProject.Controllers
{
    [Authorize("Admin")]
    public class CategoryController : Controller
    {
        private readonly IDataHelper<Category> _dataHelper;
        private int _pageItem;

        public CategoryController(IDataHelper<Category> dataHelper)
        {
            _dataHelper = dataHelper;
            _pageItem = 5;
        }

        // GET: CategoryController
        public ActionResult Index(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(_dataHelper.GetAll().Take(_pageItem));
            }
            var data = _dataHelper.GetAll().Where(x => x.Id > id).Take(_pageItem);
            return View(data);
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
        public ActionResult Create(Category collection)
        {
            try
            {
                var result = _dataHelper.Add(collection);
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

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category collection)
        {
            try
            {
                var result = _dataHelper.Update(id, collection);
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

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dataHelper.Find(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category collection)
        {
            try
            {
                var result = _dataHelper.Delete(id);
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
    }
}
