namespace tl2_tp4_2023_julian_quin;
public enum EstadosPedido
{
    Pendiente, //0
    Asignado,  //1
    Entregado, //2
    cancelado, //3   
}
public class Pedido
{
    private int numero;
    private string observacion;
    private Cliente cliente; //composicion por el enunciado
    private Cadete cadete;  //asociacion creo!
    private EstadosPedido estado;


    public int Numero { get => numero; set => numero = value; }
    public string Observacion { get => observacion; set => observacion = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }
    public EstadosPedido Estado { get => estado; set => estado = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; } // no puedo sacar los set, son nesarios para la deserializacion

    public Pedido(int numero, string observacion, EstadosPedido estado, string nombreCliente, string direccionCliente, string telCliente, string refDireccionCliente)
    {
        this.numero = numero;
        this.observacion = observacion;
        this.estado = estado;
        this.cadete = new Cadete();
        this.cliente = new Cliente(nombreCliente, direccionCliente, telCliente, refDireccionCliente);
    }
    public Pedido() { }

    public string VerDireccionCliente()
    {
        return cliente.Direccion;
    }
    public Cliente VerDatosCliente()
    {
        return cliente;
    }
    public void PedidoEntregado()
    {
        estado = EstadosPedido.Entregado;
    }
    public void PedidoCancelado()
    {
        estado = EstadosPedido.cancelado;
    }
    public void PedidoAsignado()
    {
        estado = EstadosPedido.Asignado;
    }
    public void AsignarCadete(Cadete Cadete)
    {
        cadete = Cadete;
    }
}