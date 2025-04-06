using CRUD.Data.Entidades;

namespace CRUD.Models
{
    public class HomeViewModel
    {

        public ICollection<Empleado> Empleados { get; set; }

    }
}
