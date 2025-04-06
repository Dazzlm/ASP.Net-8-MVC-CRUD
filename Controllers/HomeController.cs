using System.Diagnostics;
using CRUD.Data;
using CRUD.Data.Entidades;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly DBContext _dbContext;

        public HomeController(DBContext dbContext)
        {
            
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await _dbContext.Empleados.ToListAsync();
            HomeViewModel model = new() { Empleados = empleados };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[Route("Error/404")]
		public IActionResult Error404()
		{
			return View();
		}
	}
}
