using System.Data.SqlTypes;
using EspacioCadete;
using EspacioCadeteria;
using EspacioPedido;
using Microsoft.AspNetCore.Mvc;
//using EspacioCadeteria;

namespace tl2_tp4_2023_julian_quin.Controllers;

[ApiController]
[Route("[controller]")]
public class CadetriaController : ControllerBase
{
    private readonly ILogger<CadetriaController> _logger;
    private Cadeteria cadeteria;

    public CadetriaController(ILogger<CadetriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria();
    }

    [HttpGet("Acceso")]
    public ActionResult AccesoCadeteria (string consumo)
    {
        if (cadeteria.IniciarServicioCadeteria(consumo)) return Ok("Cadeteria Cargada");
        return BadRequest("Solicitud incorrecta");

    }
    [HttpGet("Cadetes")]
    public ActionResult<IEnumerable<Cadete>> GetCadetes()
    {
        return Ok(cadeteria.ListaCadete);
        
    }
    [HttpGet("Pedidos")]
    public ActionResult<IEnumerable<Cadete>> GetPedidos()
    {
        return Ok(cadeteria.ListaPedidos);
        
    }
    [HttpPost("Añadir_Pedidos")]
    public ActionResult<Pedido> AgregarPedido(Pedido pedido)
    {
        if (cadeteria.CrearPedido(pedido.Numero,pedido.Observacion,pedido.Estado,pedido.Cliente.Nombre,pedido.Cliente.Direccion,pedido.Cliente.Telefono,pedido.Cliente.DatosReferenciaDireccion))
        {
             
        } else return BadRequest("Error en la peticion");
        
        return Ok();
    }
    
    [HttpPut("Asignar_Pedidos")]
    public ActionResult AsignarPedido(int idPedido, int idCadete)
    {
        if (cadeteria.AsignarCadeteAPedido(idCadete,idPedido)) return Ok();
        return BadRequest("Error en la peticion");
    }
    [HttpPut("Reasignar_Pedidos")]
    public ActionResult CambiarCadetePedido(int idPedido,int idNuevoCadete)
    {
        cadeteria.ReasignarCadeteApedido(idNuevoCadete,idPedido);
        return Ok();

    }

    [HttpPut("Estado_Pedido")]
    public ActionResult CambiarEstadoPedido(int idPedido,int NuevoEstado)
    {
        bool estado;
        if(NuevoEstado==1)estado=true;
        else estado=false;
        cadeteria.CambiarEstadoPedido(idPedido,estado);
        return Ok();
    }
    [HttpGet("informe")] 
    public ActionResult GetInforme()
    {
        return Ok(cadeteria.SolicitarInforme());
    }


    
}
