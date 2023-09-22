using EspacioCadete;
using EspacioCliente;
namespace EspacioPedido
{
    public enum EstadosPedido
    {
        Entregado,
        Asignado,
        Pendiente,
        cancelado,    
    }
    public class Pedido
    {
        private int numero;
        private string observacion;
        private Cliente cliente; //composicion 
        private Cadete cadete;
        private EstadosPedido estado;
        
        
        public int Numero { get => numero; set => numero = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }
        public EstadosPedido Estado { get => estado; set => estado = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }

        public Pedido (int numero, string observacion, EstadosPedido estado, string nombreCliente, string direccionCliente, string telCliente, string refDireccionCliente)
        {
            Numero = numero;
            Observacion = observacion;
            Estado = estado;
            Cadete = new Cadete();
            Cliente = new Cliente(nombreCliente,direccionCliente,telCliente,refDireccionCliente);
        }
        public Pedido (){}

        public string VerDireccionCliente()
        {
            return Cliente.Direccion;
        }
        public Cliente VerDatosCliente()
        {
            return Cliente;
        }
        public void PedidoEntregado()
        {
            Estado = EstadosPedido.Entregado;
        }
        public void PedidoCancelado()
        {
            Estado = EstadosPedido.cancelado;
        }
        public void PedidoAsignado()
        {
            Estado = EstadosPedido.Asignado;
        }
        public void AsignarCadete(Cadete cadete)
        {
            Cadete = cadete;
        }
    }
}