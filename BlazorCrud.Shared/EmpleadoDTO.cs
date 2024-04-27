using System.ComponentModel.DataAnnotations; //validaciones

namespace BlazorCrud.Shared
{
    public class EmpleadoDTO
    {

        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string NombreCompleto { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public DateOnly FechaInicio { get; set; }

        public bool Activo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un departamento.")]
        public int IdDepartamento { get; set; }

        public DepartamentoDTO? Departamento { get; set; } //lo colocamos para que se muestre en la tabla de empleados

    }
}
