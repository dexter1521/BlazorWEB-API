using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbcrudTContext _dbContext;

        public EmpleadoController(DbcrudTContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new ResponseAPI<List<EmpleadoDTO>>();
            var listEmpleados = new List<EmpleadoDTO>();
            try
            {
                foreach (var item in await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
                {
                    listEmpleados.Add(new EmpleadoDTO
                    {
                        IdEmpleado = item.IdEmpleado,
                        NombreCompleto = item.NombreCompleto,
                        Correo = item.Correo,
                        FechaInicio = item.FechaInicio,
                        Activo = item.Activo,
                        Departamento = new DepartamentoDTO
                        {
                            IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }

                    });
                }
                response.EsCorrecto = true;
                response.Valor = listEmpleados;

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var response = new ResponseAPI<EmpleadoDTO>();
            var empleadoDTO = new EmpleadoDTO();
			
			try
			{
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(d => d.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    empleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    empleadoDTO.Correo = dbEmpleado.Correo;
                    empleadoDTO.FechaInicio = dbEmpleado.FechaInicio;
                    empleadoDTO.Activo = dbEmpleado.Activo;
                    empleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
                    empleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
					

					response.EsCorrecto = true;
                    response.Valor = empleadoDTO;
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "Valor no identificado";
                }

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
        {
            var response = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = new Empleado
                {
                    IdEmpleado = empleado.IdEmpleado,
                    NombreCompleto = empleado.NombreCompleto,
                    Correo = empleado.Correo,
                    FechaInicio = empleado.FechaInicio,
                    Activo = empleado.Activo,
                    IdDepartamento = empleado.IdDepartamento
                };

                _dbContext.Empleados.Add(dbEmpleado);
                await _dbContext.SaveChangesAsync();

                if (dbEmpleado.IdEmpleado != 0)
                {
                    response.EsCorrecto = true;
                    response.Valor = dbEmpleado.IdEmpleado;
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se pudo guardar el registro";
                }


            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(EmpleadoDTO empleado, int id)
        {
            var response = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstAsync(d => d.IdEmpleado == id);

                if (dbEmpleado.IdEmpleado != 0)
                {
                    dbEmpleado.NombreCompleto = empleado.NombreCompleto;
                    dbEmpleado.Correo = empleado.Correo;
                    dbEmpleado.FechaInicio = empleado.FechaInicio;
                    dbEmpleado.Activo = empleado.Activo;
                    dbEmpleado.IdDepartamento = empleado.IdDepartamento;                    
                    
                    _dbContext.Empleados.Update(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    response.EsCorrecto = true;
                    response.Valor = dbEmpleado.IdEmpleado;
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se pudo guardar el registro";
                }

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstAsync(d => d.IdEmpleado == id);

                if (dbEmpleado.IdEmpleado != 0)
                {

                    _dbContext.Empleados.Remove(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    response.EsCorrecto = true;
                    response.Mensaje = "Registro eliminado";
                }
                else
                {
                    response.EsCorrecto = false;
                    response.Mensaje = "No se pudo guardar el registro";
                }

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

    }
}
