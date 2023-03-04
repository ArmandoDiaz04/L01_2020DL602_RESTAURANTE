using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020DL602.Models;

namespace L01_2020DL602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public platosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto= restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<platos> listaPlatpos = (from e in _restauranteContexto.platos
                                            select e).ToList();

            if (listaPlatpos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listaPlatpos);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarPlato(int id, [FromBody] platos platoModificar)
        {

            platos? platoActual = (from e in _restauranteContexto.platos
                                           where e.platoId == id
                                           select e).FirstOrDefault();
            if (platoActual == null)
            {
                return NotFound();
            }

            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;

            _restauranteContexto.Entry(platoActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(platoModificar);

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult crearPlato([FromBody] platos plato)
        {
            try
            {
                _restauranteContexto.platos.Add(plato);
                _restauranteContexto.SaveChanges();
                return Ok(plato);
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
            platos? plato = (from e in _restauranteContexto.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (plato == null)
                return NotFound();

            _restauranteContexto.platos.Attach(plato);
            _restauranteContexto.platos.Remove(plato);
            _restauranteContexto.SaveChanges();

            return Ok(plato);
        }
    }
}
