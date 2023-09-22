using EspacioCadeteria;
using EspacioPedido;

namespace EspacioInforme
{
    public class Informe
    {
        private int totalEnviosCadetes;
        private double promedioEnviosCdts;
        private List<string> infoCadetes;
    
        public int TotalEnviosCadetes { get => totalEnviosCadetes; set => totalEnviosCadetes = value; }
        public double PromedioEnviosCdts { get => promedioEnviosCdts; set => promedioEnviosCdts = value; }
        public List<string> InfoCadetes { get => infoCadetes; set => infoCadetes = value; }

        public Informe (int TotalEntregados, double EnviosPromedios, List<string> informacionCadetes)
        {
            TotalEnviosCadetes = TotalEntregados;
            PromedioEnviosCdts=EnviosPromedios;
            infoCadetes = informacionCadetes;    
        }


    }
}