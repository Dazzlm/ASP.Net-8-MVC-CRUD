using CRUD.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD.Data.Entidades;
using CRUD.Models;
using static System.Net.Mime.MediaTypeNames;
using CRUD.Helpers;
using System.Linq;


namespace CRUD.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly DBContext _dbContext;
        private readonly IGestorImagenes _gestorImagenes;

        public EmpleadoController(DBContext dbContext, IGestorImagenes gestorImagenes)
        {
            _dbContext = dbContext;
            _gestorImagenes = gestorImagenes;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Empleados.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Detalles(int ID)
        {
            if (ID == null )
            {
                return NotFound();
            }

            Empleado empleado = await _dbContext.Empleados.FirstOrDefaultAsync(p => p.ID == ID);
			
			if (empleado == null)
			{
				return NotFound();
			}
			if (empleado == null) {
                return View();
            }
            return View(empleado);
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EmpleadoViewModel empl)
        {
            try {

                int Edad = CalcularEdad(empl);
                if (Edad == -1)
                {
                    ModelState.AddModelError(string.Empty, "Solo se aceptan Empleados Mayores de 18 años");
                    return View();
                }

                string ruta = await _gestorImagenes.SubirImagen(empl.RutaImagen);
                if (ruta == "") {
                    ModelState.AddModelError(string.Empty, "Solo se permiten archivos de tipo imagen.");
                }

                Empleado empleado = new() {
                Documento = empl.Documento,
                Nombre = empl.Nombre,
                Edad = Edad,
                Correo = empl.Correo,
                Telefono = empl.Telefono,
                Titulo = empl.Titulo,
                FechaNacimiento = empl.FechaNacimiento,
                FechaContrato = empl.FechaContrato,
                Salario = empl.Salario,
                RutaImagen = ruta,
                IsActivo = empl.IsActivo
                };

                await _dbContext.Empleados.AddAsync(empleado);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    if (dbUpdateException.InnerException.Message.Contains("Documento"))
                    {
                        ModelState.AddModelError(string.Empty, "Documento ya registrado.");

                    }
                    if (dbUpdateException.InnerException.Message.Contains("Correo"))
                    {
                        ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");

                    }
                    if (dbUpdateException.InnerException.Message.Contains("Telefono"))
                    {
                        ModelState.AddModelError(string.Empty, "Telefono ya registrado.");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int ID)
        {

            Empleado empl = await _dbContext.Empleados.FirstAsync(e => e.ID == ID);

            EmpleadoViewModel empleado = new()
            {
                ID = empl.ID,
                Documento = empl.Documento,
                Nombre = empl.Nombre,
                Correo = empl.Correo,
                Telefono = empl.Telefono,
                Titulo = empl.Titulo,
                FechaNacimiento = empl.FechaNacimiento,
                FechaContrato = empl.FechaContrato,
                Salario = empl.Salario,
                RutaImagen = null,
                DireccionImagen = empl.RutaImagen,
                IsActivo = empl.IsActivo
            };

            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EmpleadoViewModel empl)
        {
            try
            {
                int Edad = CalcularEdad(empl);
                if (Edad == -1) {
                    ModelState.AddModelError(string.Empty, "Solo se aceptan Empleados Mayores de 18 años");
                    return View();
                }
                string ruta = await _gestorImagenes.SubirImagen(empl.RutaImagen);
                if (ruta == "")
                {
                    ModelState.AddModelError(string.Empty, "Solo se permiten archivos de tipo imagen.");
                }

                Empleado empleado = await _dbContext.Empleados.FirstAsync(e => e.ID == empl.ID);

                empleado.Documento = empl.Documento;
                empleado.Nombre = empl.Nombre;
                empleado.Edad = Edad ;
                empleado.Correo = empl.Correo;
                empleado.Telefono = empl.Telefono;
                empleado.Titulo = empl.Titulo;
                empleado.FechaNacimiento = empl.FechaNacimiento;
                empleado.FechaContrato = empl.FechaContrato;
                empleado.Salario = empl.Salario;
                empleado.RutaImagen = empl.RutaImagen == null? empleado.RutaImagen :ruta;
                empleado.IsActivo = empl.IsActivo;

                _dbContext.Empleados.Update(empleado);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    if (dbUpdateException.InnerException.Message.Contains("Documento"))
                    {
                        ModelState.AddModelError(string.Empty, "Documento ya registrado.");

                    }
                    if (dbUpdateException.InnerException.Message.Contains("Correo"))
                    {
                        ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");

                    }
                    if (dbUpdateException.InnerException.Message.Contains("Telefono"))
                    {
                        ModelState.AddModelError(string.Empty, "Telefono ya registrado.");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int ID)
        {
            Empleado emp = await _dbContext.Empleados.FirstAsync(e => e.ID == ID);
            _dbContext.Empleados.Remove(emp);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private static int CalcularEdad(EmpleadoViewModel empl) {
            int Edad = DateTime.Now.Year - empl.FechaNacimiento.Year;

            if (DateTime.Now.Day < empl.FechaNacimiento.Day)
            {
                Edad--;
            }
            if (Edad < 18)
            {
                return -1;
            }
            return Edad;
        } 



    }
}