using System.Text.Json;
namespace tl2_tp4_2023_julian_quin;
public class AccesoADatosPedidos
{
    private const string path = "Json/pedidos.json";
    public List<Pedido> Obtener()
    {
        if (File.Exists(path))
        {
            string JsonEnTexto = File.ReadAllText(path);
            var Listapedidos = JsonSerializer.Deserialize<List<Pedido>>(JsonEnTexto);
            return Listapedidos;    
        }
        return new List<Pedido>();
       
    }
    public void Guardar(List<Pedido> Pedidos)
    {
        if (Pedidos!=null)
        {
            string JsonFormat = JsonSerializer.Serialize(Pedidos);
            File.WriteAllText(path, JsonFormat);    
        }
        
    }
}