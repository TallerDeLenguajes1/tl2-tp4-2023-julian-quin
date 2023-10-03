namespace tl2_tp4_2023_julian_quin;
public class Cadete
{
    private static int pedidoEntregado = 500;
    private int id;
    private string nombre;
    private string telefono;
    private string direccion;

    public static int PedidoEntregado { get => pedidoEntregado; set => pedidoEntregado = value; }
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    public Cadete(int id, string nombreCadete, string direccionCadete, string TelCadete)
    {
        this.id = id;
        this.nombre = nombreCadete;
        this.direccion = direccionCadete;
        this.telefono = TelCadete;
    }
    public Cadete() { }


}

