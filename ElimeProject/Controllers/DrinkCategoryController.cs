using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ElimeProject.Controllers
{
    [Authorize]
    public class DrinkCategoryController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IDrinkCategoryRepository _drinkCategoryRepository;

        public DrinkCategoryController(ApplicationContext context, IDrinkCategoryRepository drinkCategoryRepository)
        {
            _context = context;
            _drinkCategoryRepository = drinkCategoryRepository;
        }

        // GET: NumTables
        public async Task<IActionResult> Index()
        {
            return View(await _drinkCategoryRepository.Get());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DrinkCategory drinkCategory)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _drinkCategoryRepository.Create(drinkCategory);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(drinkCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            DrinkCategory drinkCategory = await _context.DrinkCategories.FindAsync(id);
            if (drinkCategory == null)
                return NotFound();
            return View(drinkCategory);
        }

        // POST: NumTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] DrinkCategory drinkCategory)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _drinkCategoryRepository.Update(drinkCategory);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(drinkCategory);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            DrinkCategory drinkCategory = await _context.DrinkCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinkCategory == null)
                return NotFound();
            return View(drinkCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _drinkCategoryRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NumTableExists(int id)
        {
            return _context.DrinkCategories.Any(e => e.Id == id);
        }
    }
}
