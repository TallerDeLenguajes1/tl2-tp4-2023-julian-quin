namespace tl2_tp4_2023_julian_quin;
public class Informe
{
    private int totalEnviosCadetes;
    private double promedioEnviosCdts;
    private List<InformacionDiaCadete> infoCadetes;

    public int TotalEnviosCadetes { get => totalEnviosCadetes; }
    public double PromedioEnviosCdts { get => promedioEnviosCdts; }
    public List<InformacionDiaCadete> InfoCadetes { get => infoCadetes; }

    public Informe(int TotalEntregados, double EnviosPromedios, List<InformacionDiaCadete> informacionCadetes)
    {
        this.totalEnviosCadetes = TotalEntregados;
        this.promedioEnviosCdts = EnviosPromedios;
        this.infoCadetes = informacionCadetes;
    }


}
