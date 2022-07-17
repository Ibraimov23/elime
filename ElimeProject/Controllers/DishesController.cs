using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ElimeProject.Controllers
{
    public class DishesController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IDishRepository _dishRepository;
        public DishesController(ApplicationContext context, IDishRepository dishRepository)
        {
            _context = context;
            _dishRepository = dishRepository;
        }

        public async Task<IActionResult> Index(string search, int? category)
        {
            if (!String.IsNullOrEmpty(search) && search.Length > 2)
                return PartialView(await _dishRepository.GetSearch(search));
            if (category != null && category != 0)
                return PartialView(await _dishRepository.GetCategory(category));
            ViewData["Instagram"] = "@elimeservice";
            ViewData["Layout"] = "_Layout2";
            ViewData["color"] = "background-color: #f4f4f4;";
            return View(await _dishRepository.Get());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.DishCategories, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NutritValue,Weight,Description,Price,DishCategoryId,Image")] Dish dish)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _dishRepository.Create(dish);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                ViewData["Categories"] = new SelectList(_context.DishCategories, "Id", "Name", dish.DishCategoryId);
            return View(dish);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Dish dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
                return NotFound();
            return View(dish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,NutritValue,Weight,Description,Price,DishCategoryId,Image")] Dish dish)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _dishRepository.Update(dish);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(dish);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Dish dish = await _context.Dishes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
                return NotFound();
            else
                return View(dish);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dishRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}
