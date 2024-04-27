using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Models;

[Table("departamento")]
public partial class Departamento
{
    [Key]
    [Column("idDepartamento")]
    public int IdDepartamento { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [InverseProperty("IdDepartamentoNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    [InverseProperty("IdDepartamentoNavigation")]
    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
