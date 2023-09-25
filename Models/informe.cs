using EspacioCadeteria;

namespace EspacioInforme
{
    public class Informe
    {
        private int totalEnviosCadetes;
        private double promedioEnviosCdts;
        private List<Inf_Personal_cad> infoCadetes;
    
        public int TotalEnviosCadetes { get => totalEnviosCadetes; set => totalEnviosCadetes = value; }
        public double PromedioEnviosCdts { get => promedioEnviosCdts; set => promedioEnviosCdts = value; }
        public List<Inf_Personal_cad> InfoCadetes { get => infoCadetes; set => infoCadetes = value; }

        public Informe (int TotalEntregados, double EnviosPromedios, List<Inf_Personal_cad> informacionCadetes)
        {
            TotalEnviosCadetes = TotalEntregados;
            PromedioEnviosCdts=EnviosPromedios;
            InfoCadetes = informacionCadetes;    
        }


    }
}