using EspacioCadete;
using EspacioInforme;
using EspacioPedido;
//using EspacioAcceso;
using EspacioAccesoADatosPedidos;
namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private static Cadeteria CadeteriaSingleton;
        const int Entregado = 500;
        private string nombre;
        private string telefonoCadeteria;
        private List<Cadete> listaCadete; // agregacion
        private List<Pedido> listaPedidos; //composicion ?
        private AccesoADatosPedidos accessDataPedidos;
        

        public string Nombre { get => nombre; set => nombre = value; }
        public string TelefonoCadeteria { get => telefonoCadeteria; set => telefonoCadeteria = value; }
        public List<Cadete> ListaCadete { get => listaCadete; set => listaCadete = value; }
        public List<Pedido> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
        public AccesoADatosPedidos AccessDataPedidos { get => accessDataPedidos; set => accessDataPedidos = value; }

        public Cadeteria(string NombreCadeteria, string telCadeteria, List<Cadete> cadetesLista, AccesoADatosPedidos accessDataPedidos)
        {
            ListaCadete = cadetesLista;
            Nombre = NombreCadeteria;
            telefonoCadeteria = telCadeteria;
            listaPedidos = new List<Pedido>(); //error sino instancio!!
            this.AccessDataPedidos = accessDataPedidos;
        }
        public static Cadeteria GetCadeteria(string NombreCadeteria, string telCadeteria, List<Cadete> cadetesLista, AccesoADatosPedidos accessDataPedidos) //metodo estatico
        {
            if (CadeteriaSingleton == null)
            {
                CadeteriaSingleton= new(NombreCadeteria,telCadeteria,cadetesLista,accessDataPedidos); //patron singleton
            }
            return CadeteriaSingleton;
        }
        public Cadeteria() { }


        // public static bool IniciarServicioCadeteria(string consumo)
        // {
        //     AccesoADatos Acceso;
        //     string pathCadetes = "", pathCadeteria = "";
        //     switch (consumo.ToLower())
        //     {
        //         case "json":
        //             Acceso = new AccesoADatos_Json();
        //             pathCadeteria = "DatosCadeteria.json";
        //             pathCadetes = "DatosCadetes.json";
        //             break;
        //         case "csv":
        //             Acceso = new AccesoADatos_Csv();
        //             pathCadeteria = "DatosCadeteria.csv";
        //             pathCadetes = "DatosCadetes.csv";
        //             break;
        //         default:
        //             CadeteriaSingleton = new Cadeteria();
        //             Acceso = new AccesoADatos_Json();
        //             break;
        //     }
        //     if (Acceso.PathExist(pathCadeteria) && Acceso.PathExist(pathCadetes))
        //     {
        //         CadeteriaSingleton = new Cadeteria(Acceso.InfoCadeteria(pathCadeteria).Nombre, Acceso.InfoCadeteria(pathCadeteria).TelefonoCadeteria, Acceso.ObtenerCadetes(pathCadetes));
        //         return true;
        //     }
        //     return false;


        // }

        public bool CrearPedido(int numeroP, string observacionP, EstadosPedido estadoP, string nombreC, string direccionC, string telC, string refDireccionC)
        {
            var nuevoPedido = new Pedido(numeroP, observacionP, estadoP, nombreC, direccionC, telC, refDireccionC);
            if (nuevoPedido != null)
            {
                ListaPedidos.Add(nuevoPedido);
                accessDataPedidos.Guardar(ListaPedidos);
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
                    accessDataPedidos.Guardar(ListaPedidos);
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
                if (PedidoEncontado.Estado != EstadosPedido.Entregado && PedidoEncontado.Estado != EstadosPedido.cancelado){
                    PedidoEncontado.AsignarCadete(CadeteEncontrado);
                    accessDataPedidos.Guardar(ListaPedidos);
                    return true;
                } else return false;
                
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
                    accessDataPedidos.Guardar(ListaPedidos);
                    flag = true;                            
                }
                else
                {
                    if (PedidoEncontrado.Estado != EstadosPedido.Entregado && nuevoEstado == 0) //con 0 indico que se canceló
                    {
                        PedidoEncontrado.PedidoCancelado();
                        flag = true;
                    }
                }
            }
            return flag;
        }
        public double JornalACobrar(int idCadete)
        {
            double jornal = 0;

            foreach (var pedido in ListaPedidos)
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
            foreach (var cadete in ListaCadete)
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
            foreach (var pedido in ListaPedidos)
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
            int CantidadCadetes = ListaCadete.Count();
            int TotalPedidosEntregados = ListaPedidos.Count(pedido => pedido.Estado == EstadosPedido.Entregado);
            double EnviosPromediosCadetes = TotalPedidosEntregados / (double)CantidadCadetes;
            List<Inf_Personal_cad> InformacionCadete = new();
            var DiaEmpleados = ListaCadete.Select(cadete => new
            {
                NombreCadete = cadete.Nombre,
                TotalCadete = JornalACobrar(cadete.Id),
                PedidosEntregados = ListaPedidos.Count(pedido => pedido.Cadete.Id == cadete.Id && pedido.Estado == EstadosPedido.Entregado)
            });
            foreach (var DiaCadete in DiaEmpleados)
            {
                var infcadete = new Inf_Personal_cad();
                infcadete.NombreCadete = DiaCadete.NombreCadete;
                infcadete.PedidosEntregados = DiaCadete.PedidosEntregados;
                infcadete.TotalCadete = DiaCadete.TotalCadete;

                InformacionCadete.Add(infcadete);
            }

            return new Informe(TotalPedidosEntregados, EnviosPromediosCadetes, InformacionCadete);

        }


    }
    public class Inf_Personal_cad
    {
        private string nombreCadete;
        private double totalCadete;
        private int pedidosEntregados;

        public string NombreCadete { get => nombreCadete; set => nombreCadete = value; }

        public int PedidosEntregados { get => pedidosEntregados; set => pedidosEntregados = value; }
        public double TotalCadete { get => totalCadete; set => totalCadete = value; }
    }
}