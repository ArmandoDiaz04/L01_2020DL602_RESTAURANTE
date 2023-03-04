using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020DL602.Models;

namespace L01_2020DL602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristasController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public motoristasController(restauranteContext restauranteContexto)
        {
            _restauranteContexto= restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<motoristas> ListadoMotorista = (from e in _restauranteContexto.motoristas
                                                         select e).ToList();

            if(ListadoMotorista.Count() == 0)
            {
                return NotFound();
            }

            return Ok(ListadoMotorista);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarMotorista (int id, [FromBody]motoristas motoristaModificar) {

            motoristas? motoristaActual = (from e in _restauranteContexto.motoristas
                                           where e.motoristaId == id
                                           select e).FirstOrDefault();
            if (motoristaActual == null)
            {
                return NotFound();
            }

            motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;

            _restauranteContexto.Entry(motoristaActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(motoristaModificar);

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult crearMotorista([FromBody] motoristas motorista)
        {
            try
            {
                _restauranteContexto.motoristas.Add(motorista);
                _restauranteContexto.SaveChanges();
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //eliminar
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarMotorista(int id)
        {
            motoristas? motorista = (from e in _restauranteContexto.motoristas
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (motorista == null)
                return NotFound();

            _restauranteContexto.motoristas.Attach(motorista);
            _restauranteContexto.motoristas.Remove(motorista);
            _restauranteContexto.SaveChanges();

            return Ok(motorista);
        }
    }
}
