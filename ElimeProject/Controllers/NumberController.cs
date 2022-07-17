using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElimeProject.Controllers
{
    public class NumberController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly INumberRepository _numberRepository;
        private readonly string numberKey = "number";
        public NumberController(ApplicationContext context, INumberRepository numberRepository)
        {
            _context = context;
            _numberRepository = numberRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Register(int? id)
        {
            NumTable num = await _numberRepository.Number(id);
            if (num != null)
            {
                HttpContext.Response.Cookies.Delete(numberKey);
                string json = System.Text.Json.JsonSerializer.Serialize<NumTable>(num);
                HttpContext.Response.Cookies.Append(numberKey, json);
                HttpContext.Response.Cookies.Append("remove", "success");
            }
            return RedirectToAction("Index", "Dishes");
        }
    }
}
