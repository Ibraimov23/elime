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
    public class DishCategoryController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IDishCategoryRepository _dishCategoryRepository;

        public DishCategoryController(ApplicationContext context, IDishCategoryRepository dishCategoryRepository)
        {
            _context = context;
            _dishCategoryRepository = dishCategoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dishCategoryRepository.Get());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DishCategory dishCategory)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _dishCategoryRepository.Create(dishCategory);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(dishCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            DishCategory dishCategory = await _context.DishCategories.FindAsync(id);
            if (dishCategory == null)
                return NotFound();
            return View(dishCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] DishCategory dishCategory)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _dishCategoryRepository.Update(dishCategory);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(dishCategory);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            DishCategory dishCategory = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishCategory == null)
                return NotFound();
            return View(dishCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dishCategoryRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NumTableExists(int id)
        {
            return _context.DishCategories.Any(e => e.Id == id);
        }
    }
}
