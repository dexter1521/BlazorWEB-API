using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Models;

[Table("Empleado")]
public partial class Empleado
{
    [Key]
    public int IdEmpleado { get; set; }

    [StringLength(50)]
    public string NombreCompleto { get; set; } = null!;

    [StringLength(50)]
    public string Correo { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public bool Activo { get; set; }

    [Column("idDepartamento")]
    public int IdDepartamento { get; set; }

    [ForeignKey("IdDepartamento")]
    [InverseProperty("Empleados")]
    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
