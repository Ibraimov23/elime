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
    public class NumTablesController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly INumTableRepository _numTableRepository;

        public NumTablesController(ApplicationContext context, INumTableRepository numTableRepository)
        {
            _context = context;
            _numTableRepository = numTableRepository;
        }

        // GET: NumTables
        public async Task<IActionResult> Index()
        {
            return View(await _numTableRepository.Get());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number")] NumTable numTable)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _numTableRepository.Create(numTable);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(numTable);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            NumTable numTable = await _context.NumTables.FindAsync(id);
            if (numTable == null)
                return NotFound();
            else
                return View(numTable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Number")] NumTable numTable)
        {
            if (ModelState.IsValid)
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _numTableRepository.Update(numTable);
                    scope.Complete();
                    return RedirectToAction(nameof(Index));
                }
            else
                return View(numTable);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            NumTable numTable = await _context.NumTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (numTable == null)
                return NotFound();
            else
                return View(numTable);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _numTableRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NumTableExists(int id)
        {
            return _context.NumTables.Any(e => e.Id == id);
        }
    }
}
