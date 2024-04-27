using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        private readonly DbcrudTContext _dbContext;

        public DepartamentoController(DbcrudTContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new ResponseAPI<List<DepartamentoDTO>>();
            var listDepartamentos = new List<DepartamentoDTO>();
            try
            {
                foreach (var item in await _dbContext.Departamentos.ToListAsync())
                {
                    listDepartamentos.Add(new DepartamentoDTO
                    {
                        IdDepartamento = item.IdDepartamento,
                        Nombre = item.Nombre
                    });
                }

                response.EsCorrecto = true;
                response.Valor = listDepartamentos;

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
