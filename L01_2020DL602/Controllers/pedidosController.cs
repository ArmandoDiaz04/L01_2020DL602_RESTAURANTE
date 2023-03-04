using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020DL602.Models;

namespace L01_2020DL602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public pedidosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;

        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<pedidos> ListadoPedidos = (from e in _restauranteContexto.pedidos
                                                 select e).ToList();

            if (ListadoPedidos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPedidos);
        }



        //Crear
        [HttpPost]
        [Route("Add")]

        public IActionResult guardarPedido([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteContexto.pedidos.Add(pedido);
                _restauranteContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarPedido (int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual =  (from e in _restauranteContexto.pedidos
                                      where e.pedidoId== id
                                      select e).FirstOrDefault();

            if (pedidoActual==null)
            {
                return NotFound();
            }

            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContexto.Entry(pedidoActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(pedidoModificar);
        }

        //eliminar
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarPedido(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.pedidoId== id
                               select e).FirstOrDefault();

            if (pedido == null)
                return NotFound();

            _restauranteContexto.pedidos.Attach(pedido);
            _restauranteContexto.pedidos.Remove(pedido);
            _restauranteContexto.SaveChanges();

            return Ok(pedido);
        }
        [HttpGet]
        [Route("GetPedidoByCliente")]
        public IActionResult pedidosCliente(int clienteId)
        {
            List<pedidos> ListadoPedidos = (from e in _restauranteContexto.pedidos
                                            where e.clienteId == clienteId
                                            select e).ToList();

            if (ListadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPedidos);
        }


        

        [HttpGet]
        [Route("GetPedidoByMotorista")]
        public IActionResult pedidosMotorista(int motoristaId)
        {
            List<pedidos> ListadoPedidos = (from e in _restauranteContexto.pedidos
                                            where e.motoristaId == motoristaId
                                            select e).ToList();

            if (ListadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPedidos);
        }





    }
}
