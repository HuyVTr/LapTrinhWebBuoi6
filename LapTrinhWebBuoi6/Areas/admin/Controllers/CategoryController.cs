using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LapTrinhWebBuoi6.Models;

namespace LapTrinhWebBuoi6.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        // Dùng static list để mô phỏng database
        private static List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại", Description = "Thiết bị di động" },
            new Category { Id = 2, Name = "Laptop", Description = "Máy tính cá nhân" }
        };

        public IActionResult Index()
        {
            return View(_categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = _categories.Max(c => c.Id) + 1;
                _categories.Add(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var existing = _categories.FirstOrDefault(c => c.Id == category.Id);
                if (existing == null) return NotFound();

                existing.Name = category.Name;
                existing.Description = category.Description;

                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
            return RedirectToAction("Index");
        }
    }
}
