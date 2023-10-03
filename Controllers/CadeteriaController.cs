using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2023_julian_quin.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly ILogger<CadeteriaController> _logger;
    private Cadeteria cadeteria;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria();
    }

   
    [HttpGet("Ver_Cadetes")]
    public ActionResult<IEnumerable<Cadete>> GetCadetes()
    {
        return Ok(cadeteria.ListaCadete);
        
    }
    [HttpGet("Ver_Pedidos")]
    public ActionResult<IEnumerable<Cadete>> GetPedidos()
    {
        return Ok(cadeteria.ListaPedidos);
        
    }

    [HttpGet("Ver_Pedido_Id")]
    public ActionResult<Pedido> GetPedidoId(int id)
    {
        if (id>=0)
        {
            var pedido = cadeteria.EncontrarPedido(id);
            if (pedido!=null)
            {
                return Ok(pedido);
            }
        }
        return NotFound("Error en la solicitud");

    }
    [HttpGet("Ver_Cadete_Id")]
    public ActionResult<Pedido> GetCadeteId(int id)
    {
        if (id>=0)
        {
            var cadete = cadeteria.EncontrarCadetePorId(id);
            if (cadete!=null)
            {
                return Ok(cadete);
            }
        }
        return NotFound("Error en la solicitud");

    }
    [HttpPost("Add_Pedidos")]
    public ActionResult<Cadete> AgregarPedido(Pedido pedido)
    {
        
        if (pedido != null)
        {
            int numPedido;
            if(cadeteria.ListaPedidos.Count==0) numPedido = 0;
            else {
                numPedido = cadeteria.ListaPedidos.Max(pedido => pedido.Numero)+1;
                pedido.Numero = numPedido;
            }
            if(cadeteria.CrearPedido(pedido.Numero,pedido.Observacion,pedido.Estado,pedido.Cliente.Nombre,pedido.Cliente.Direccion,pedido.Cliente.Telefono,pedido.Cliente.DatosReferenciaDireccion)) return Ok(pedido);
            else return BadRequest("Solicitud incorrecta");
             
        } else return BadRequest("Solicitud incorrecta");
        
       
    }
    
    [HttpPut("Asignar_Pedidos")]
    public ActionResult AsignarPedido(int idPedido, int idCadete)
    {
        if (cadeteria.AsignarCadeteAPedido(idCadete,idPedido)) return Ok("Pedido asignado");
        return NotFound("Resurso no encontrado");
    }
    [HttpPut("Reasignar_Pedidos")]
    public ActionResult CambiarCadetePedido(int idPedido,int idNuevoCadete)
    {
        if(cadeteria.ReasignarCadeteApedido(idNuevoCadete,idPedido)) return Ok("Pedido Reasignado");
        return BadRequest("Rescurso no encontrado");

    }

    [HttpPut("Modificar_Estado_Pedido")]
    public ActionResult CambiarEstadoPedido(int idPedido,int nuevoEstado)
    {
        if(cadeteria.CambiarEstadoPedido(idPedido,nuevoEstado)) return Ok("Nuevo Estado establecido");
        return NotFound("Error en la solicitud: posible id o Nuevo estado incorrecto para el pedido");
        
    }
    [HttpPost("Agregar_Cadetes")]
    public ActionResult<Cadete> AgregarCadete (Cadete nuevoCadete)
    {
        if (nuevoCadete!=null)
        {
            int id = cadeteria.ListaCadete.Max(cadete => cadete.Id)+1;
            cadeteria.AgregarCadetes(id,nuevoCadete.Nombre,nuevoCadete.Direccion,nuevoCadete.Telefono);
            return Ok(nuevoCadete);
        }else return BadRequest();

    }
    [HttpGet("Informe")] 
    public ActionResult GetInforme()
    {
        return Ok(cadeteria.SolicitarInforme());
    }
   
}
