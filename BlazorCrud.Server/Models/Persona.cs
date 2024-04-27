using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Models;

[Table("persona")]
public partial class Persona
{
    [Key]
    [Column("idPersona")]
    public int IdPersona { get; set; }

    [Column("nombreCompleto")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NombreCompleto { get; set; }

    [Column("idDepartamento")]
    public int? IdDepartamento { get; set; }

    [Column("sueldo")]
    public int? Sueldo { get; set; }

    [Column("fechaContrato")]
    public DateOnly? FechaContrato { get; set; }

    [ForeignKey("IdDepartamento")]
    [InverseProperty("Personas")]
    public virtual Departamento? IdDepartamentoNavigation { get; set; }
}
