namespace tl2_tp4_2023_julian_quin;
public class Cadeteria
{
    private static Cadeteria cadeteriaSingleton;
    const int Entregado = 500;
    private string nombre;
    private string telefonoCadeteria;
    private List<Cadete> listaCadete; // agregacion
    private List<Pedido> listaPedidos; //composicion ?
    private AccesoADatosCadetes accesoCadetes;
    private AccesoADatosPedidos accesoPedido;


    public string Nombre { get => nombre; set => nombre = value; }
    public string TelefonoCadeteria { get => telefonoCadeteria; set => telefonoCadeteria = value; }
    public List<Cadete> ListaCadete { get => listaCadete;}
    public List<Pedido> ListaPedidos { get => listaPedidos;}
    public Cadeteria() {
        this.accesoCadetes = new AccesoADatosCadetes();
        this.accesoPedido = new AccesoADatosPedidos();
    }

    public static Cadeteria GetCadeteria()
    {
        if (cadeteriaSingleton == null)
        {
            var accesoDataCad = new AccesoADatosCadeteria();
            cadeteriaSingleton = accesoDataCad.Obtener();
            cadeteriaSingleton.CargarCadetes();
            cadeteriaSingleton.CargarPedidos();
            //podria acceder a los campos acceso cad y ped y realizar las respectivas instancias (lo hago en el constructor)
            // asi... 
            // cadeteriaSingleton.accesoCadetes = new AccesoADatosCadetes();// creo instancia para el campo
            // cadeteriaSingleton.accesoPedido = new AccesoADatosPedidos();
        }
        return cadeteriaSingleton;

    }
    private void CargarPedidos()
    {
        listaPedidos = accesoPedido.Obtener();
    }
    private void CargarCadetes()
    {
        listaCadete = accesoCadetes.Obtener();
    }
    public bool CrearPedido(int numeroP, string observacionP, EstadosPedido estadoP, string nombreC, string direccionC, string telC, string refDireccionC)
    {
        var nuevoPedido = new Pedido(numeroP, observacionP, estadoP, nombreC, direccionC, telC, refDireccionC);
        if (nuevoPedido != null)
        {
            listaPedidos.Add(nuevoPedido);
            accesoPedido.Guardar(listaPedidos);
            return true;
        }

        return false;
    }

    public bool AsignarCadeteAPedido(int idCadete, int numeroP)
    {
        var CadeteEncontrado = EncontrarCadetePorId(idCadete);
        var PedidoEncontado = EncontrarPedido(numeroP);
        if (CadeteEncontrado != null && PedidoEncontado != null)
        {
            if (PedidoEncontado.Estado == EstadosPedido.Pendiente)
            {
                PedidoEncontado.AsignarCadete(CadeteEncontrado);
                PedidoEncontado.PedidoAsignado(); // cambio el estado del pedido a "asignado"
                accesoPedido.Guardar(listaPedidos);
            }
            else return false;
            return true;
        }
        return false;
    }
    public bool ReasignarCadeteApedido(int idCadete, int numeroP)
    {
        var CadeteEncontrado = EncontrarCadetePorId(idCadete);
        var PedidoEncontado = EncontrarPedido(numeroP);
        if (CadeteEncontrado != null && PedidoEncontado != null)
        {
            if (PedidoEncontado.Estado != EstadosPedido.Entregado && PedidoEncontado.Estado != EstadosPedido.cancelado)
            {
                PedidoEncontado.AsignarCadete(CadeteEncontrado);
                accesoPedido.Guardar(listaPedidos);
                return true;
            }
            else return false;

        }
        return false;
    }
    public bool CambiarEstadoPedido(int numeroP, int nuevoEstado)
    {
        bool flag = false; // false = cambioFallido , true = cambioRealizado
        var PedidoEncontrado = EncontrarPedido(numeroP);
        if (PedidoEncontrado != null)
        {
            if (PedidoEncontrado.Estado == EstadosPedido.Asignado && nuevoEstado == 1) //con 1 se avisa que se entregó
            {
                PedidoEncontrado.PedidoEntregado();
                accesoPedido.Guardar(listaPedidos);
                flag = true;
            }
            else
            {
                if (PedidoEncontrado.Estado != EstadosPedido.Entregado && nuevoEstado == 0) //con 0 indico que se canceló
                {
                    PedidoEncontrado.PedidoCancelado();
                    accesoPedido.Guardar(listaPedidos);
                    flag = true;
                }
            }
        }
        return flag;
    }
    public double JornalACobrar(int idCadete)
    {
        double jornal = 0;

        foreach (var pedido in listaPedidos)
        {
            if ((pedido.Cadete.Id == idCadete) && (pedido.Estado == EstadosPedido.Entregado))
            {
                jornal += Entregado;
            }

        }
        return jornal;

    }

    private Cadete EncontrarCadetePorId(int idCadete)
    {
        foreach (var cadete in listaCadete)
        {
            if (cadete.Id == idCadete)
            {
                return cadete;
            }
        }
        return null;

    }
    private Pedido EncontrarPedido(int numeroP)
    {
        foreach (var pedido in listaPedidos)
        {
            if (numeroP == pedido.Numero)
            {
                return pedido;
            }

        }
        return null;

    }
    public Informe SolicitarInforme()
    {
        int CantidadCadetes = listaCadete.Count();
        int TotalPedidosEntregados = listaPedidos.Count(pedido => pedido.Estado == EstadosPedido.Entregado);
        double EnviosPromediosCadetes = TotalPedidosEntregados / (double)CantidadCadetes;
        List<InformacionDiaCadete> InformacionCadete = new();
        var DiaEmpleados = listaCadete.Select(cadete => new
        {
            NombreCadete = cadete.Nombre,
            TotalCadete = JornalACobrar(cadete.Id),
            PedidosEntregados = listaPedidos.Count(pedido => pedido.Cadete.Id == cadete.Id && pedido.Estado == EstadosPedido.Entregado)
        });

       
        foreach (var DiaCadete in DiaEmpleados)
        {
            var infcadete = new InformacionDiaCadete();
            infcadete.NombreCadete = DiaCadete.NombreCadete;
            infcadete.PedidosEntregados = DiaCadete.PedidosEntregados;
            infcadete.TotalCadete = DiaCadete.TotalCadete;

            InformacionCadete.Add(infcadete);
        }

        return new Informe(TotalPedidosEntregados, EnviosPromediosCadetes, InformacionCadete);

    }


}
public class InformacionDiaCadete
{
    private string nombreCadete;
    private double totalCadete;
    private int pedidosEntregados;

    public string NombreCadete { get => nombreCadete; set => nombreCadete = value; }

    public int PedidosEntregados { get => pedidosEntregados; set => pedidosEntregados = value; }
    public double TotalCadete { get => totalCadete; set => totalCadete = value; }
}
