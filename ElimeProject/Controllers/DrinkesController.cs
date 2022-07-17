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
    public class DrinkesController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IDrinkRepository _drinkRepository;
        public DrinkesController(ApplicationContext context, IDrinkRepository drinkRepository)
        {
            _context = context;
            _drinkRepository = drinkRepository;
        }
        public async Task<IActionResult> Index(string search, int? category)
        {
            if (!String.IsNullOrEmpty(search) && search.Length > 2)
                return PartialView(await _drinkRepository.GetSearch(search));
            if (category != null && category != 0)
                return PartialView(await _drinkRepository.GetCategory(category));
            ViewData["Layout"] = "_Layout2";
            ViewData["color"] = "background-color: #f4f4f4;";
            return View(await _drinkRepository.Get());
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.DrinkCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NutritValue,Milliliter,Description,Price,DrinkCategoryId,Image")] Drink drink)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _drinkRepository.Create(drink);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                ViewData["Categories"] = new SelectList(_context.DrinkCategories, "Id", "Name", drink.DrinkCategoryId);
            return View(drink);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Drink drink = await _context.Drinkes.FindAsync(id);
            if (drink == null)
                return NotFound();
            return View(drink);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,NutritValue,Milliliter,Description,Price,DrinkCategoryId,Image")] Drink drink)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _drinkRepository.Update(drink);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(drink);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Drink drink = await _context.Drinkes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
                return NotFound();
            else
                return View(drink);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _drinkRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        private bool DishExists(int id)
        {
            return _context.Drinkes.Any(e => e.Id == id);
        }
    }
}

