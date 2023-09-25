using System.Data.SqlTypes;
using EspacioAccesoADatosPedidos;
using EspacioCadete;
using EspacioCadeteria;
using EspacioDatosCadeteria;
using EspacioDatosCadetes;
using EspacioPedido;
using Microsoft.AspNetCore.Mvc;
//using EspacioCadeteria;

namespace tl2_tp4_2023_julian_quin.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly ILogger<CadeteriaController> _logger;
    private Cadeteria cadeteria;
    private AccesoADatosPedidos accesoPedido;
    private AccesoADatosCadeteria accesoDatosCadeteria;
    private AccesoADatosCadetes accesoDatosCadetes;


    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        accesoDatosCadeteria = new ();
        accesoDatosCadetes = new();
        accesoPedido = new();
        cadeteria = Cadeteria.GetCadeteria(accesoDatosCadeteria.Obtener().Nombre,accesoDatosCadeteria.Obtener().TelefonoCadeteria,accesoDatosCadetes.Obtener(),accesoPedido);
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
    [HttpPost("Add_Pedidos")]
    public ActionResult<Pedido> AgregarPedido(Pedido pedido)
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
    [HttpGet("Informe")] 
    public ActionResult GetInforme()
    {
        return Ok(cadeteria.SolicitarInforme());
    }
   
}
