using CRUD.Models;

namespace CRUD.Helpers
{
    public interface IGestorImagenes
    {
         Task<string> SubirImagen (IFormFile empl);
    }
}
