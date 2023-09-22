using EspacioCadete;
using EspacioInforme;
using EspacioPedido;
using EspacioAcceso;
namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private static Cadeteria CadeteriaSingleton;
        const int Entregado = 500;
        private string nombre;
        private string telefonoCadeteria;
        private List<Cadete> listaCadete;
        private List<Pedido> listaPedidos;

        public string Nombre { get => nombre; set => nombre = value; }
        public string TelefonoCadeteria { get => telefonoCadeteria; set => telefonoCadeteria = value; }
        public List<Cadete> ListaCadete { get => listaCadete; set => listaCadete = value; }
        public List<Pedido> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
        public Cadeteria(string NombreCadeteria, string telCadeteria, List<Cadete> cadetesLista)
        {
            ListaCadete = cadetesLista;
            Nombre = NombreCadeteria;
            telefonoCadeteria = telCadeteria;
            listaPedidos = new List<Pedido>(); //error sino instancio!!
        }
        public static Cadeteria GetCadeteria() //metodo estatico
        {
            if (CadeteriaSingleton == null)
            {
                CadeteriaSingleton = new Cadeteria(); //patron singleton
            }
            return CadeteriaSingleton;
        }
        public Cadeteria() { }


        public bool IniciarServicioCadeteria(string consumo)
        {
            AccesoADatos Acceso;
            string pathCadetes, pathCadeteria;
            if (consumo.ToLower() == "json")
            {
                Acceso = new AccesoADatos_Json();
                pathCadeteria = "DatosCadeteria.json";
                pathCadetes = "DatosCadetes.json";
            }
            else{
            
                Acceso = new AccesoADatos_Csv();
                pathCadeteria = "DatosCadeteria.csv";
                pathCadetes = "DatosCadetes.csv";
            }
            if (Acceso.PathExist(pathCadeteria) && Acceso.PathExist(pathCadetes))
            {
                CadeteriaSingleton = new Cadeteria(Acceso.InfoCadeteria(pathCadeteria).Nombre, Acceso.InfoCadeteria(pathCadeteria).TelefonoCadeteria, Acceso.ObtenerCadetes(pathCadetes));
                return true;
            }
            return false;


        }

        public bool CrearPedido(int numeroP, string observacionP, EstadosPedido estadoP, string nombreC, string direccionC, string telC, string refDireccionC)
        {
            var nuevoPedido = new Pedido(numeroP, observacionP, estadoP, nombreC, direccionC, telC, refDireccionC);
            if (nuevoPedido != null)
            {
                ListaPedidos.Add(nuevoPedido);
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
                    PedidoEncontado.PedidoAsignado();// cambio el estado del pedido a "asignado"
                }
                else return false;
                return true;
            }
            return false;
        }
        private void EliminarPedido(int numeroP) // no es necesario, pero lo dejo!
        {
            var PedidoEncontrado = EncontrarPedido(numeroP);
            if (PedidoEncontrado != null)
            {
                ListaPedidos.Remove(PedidoEncontrado);
            }
        }
        public bool ReasignarCadeteApedido(int idCadete, int numeroP)
        {
            var CadeteEncontrado = EncontrarCadetePorId(idCadete);
            var PedidoEncontado = EncontrarPedido(numeroP);
            if (CadeteEncontrado != null && PedidoEncontado != null)
            {
                if (PedidoEncontado.Estado != EstadosPedido.Entregado && PedidoEncontado.Estado != EstadosPedido.cancelado) PedidoEncontado.AsignarCadete(CadeteEncontrado);
                else return false;
                return true;
            }
            return false;
        }
        public int CambiarEstadoPedido(int numeroP, bool opcion)
        {
            int warning = 2;
            var PedidoEncontrado = EncontrarPedido(numeroP);
            if (PedidoEncontrado != null)
            {
                if (PedidoEncontrado.Estado == EstadosPedido.Asignado && opcion == true) //con true se avisa que se entregó
                {
                    PedidoEncontrado.PedidoEntregado();
                    warning = 1; // aviso que se cambio exitosamente
                }
                else warning = 0; // para indicar que el pedido no está asignado

                if (opcion == false)
                { // no discrimino si está asignado para cancelar (false == cancelar)
                    PedidoEncontrado.PedidoCancelado();
                    warning = 1; // aviso que se cambio exitosamente
                }
                return warning;
            }
            return warning; // para indicar que el numero de pedido es incorrecto (por defecto 2)
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
            List<string> LaburoDiaCadetesInfo = new();
            var DiaEmpleados = ListaCadete.Select(cadete => new
            {
                NombreCadete = cadete.Nombre,
                TotalCadete = JornalACobrar(cadete.Id),
                PedidosEntregados = ListaPedidos.Count(pedido => pedido.Cadete.Id == cadete.Id && pedido.Estado == EstadosPedido.Entregado)
            });
            foreach (var DiaCadete in DiaEmpleados)
            {
                var infoCadete = "Cadete: " + DiaCadete.NombreCadete + " " + ", Cobro: $" + DiaCadete.TotalCadete + " P.Entregados: " + DiaCadete.PedidosEntregados;
                LaburoDiaCadetesInfo.Add(infoCadete);
            }
            //List<string> LaburoDeLDiaCadetes = ListaCadete.Select(cadete => $"{cadete.Nombre}, Total a Cobrar: {JornalACobrar(cadete.Id)}, Pedidos Entregados: {ListaPedidos.Count(pedido => pedido.Cadete.Id == cadete.Id && pedido.Estado == EstadosPedido.Entregado)}").ToList();

            return new Informe(TotalPedidosEntregados, EnviosPromediosCadetes, LaburoDiaCadetesInfo);

        }


    }

}