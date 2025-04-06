using System.IO;
using System.Linq;
using CRUD.Models;

namespace CRUD.Helpers
{
    public class GestorImagenes : IGestorImagenes
    {
        public async Task<string> SubirImagen(IFormFile RutaImagen)
        {
            if (RutaImagen != null)
            {
                var TipoExtensiones = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var Extension = Path.GetExtension(RutaImagen.FileName).ToLower();

                if (!TipoExtensiones.Contains(Extension))
                {
                    return "";
                }
                string DireccionCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imagen");

                string NombreFoto = Guid.NewGuid().ToString() + "_" + RutaImagen.FileName;

                string RutaCompleta = Path.Combine(DireccionCarpeta, NombreFoto);

                using (var fileStream = new FileStream(RutaCompleta, FileMode.Create))
                {
                    await RutaImagen.CopyToAsync(fileStream);
                }
                return "\\Imagen\\" + NombreFoto;
            }
            else
            { 
                return "\\Imagen\\NoDisponible.png";
            }
        }
    }
}
