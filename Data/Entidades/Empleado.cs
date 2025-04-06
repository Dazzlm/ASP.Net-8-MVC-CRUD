using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Data.Entidades
{
    public class Empleado
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ID { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Documento { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Edad")]
        [Range(18, int.MaxValue, ErrorMessage = "Ingresa una edad valida.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Edad { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debes ingresar un correo válido.")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Display(Name = "Telefono")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Telefono { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        public DateOnly FechaNacimiento { get; set; }

        [Display(Name = "Fecha de Contrato")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        public DateOnly FechaContrato { get; set; }

        [Display(Name = "Salario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Salario { get; set; }
        [Display(Name = "Imagen")]
        public string? RutaImagen { get; set; }

        public bool IsActivo { get; set; }

    }
}
