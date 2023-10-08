using System.Text.Json;
namespace tl2_tp4_2023_julian_quin;
public class AccesoADatosPedidos
{
    private const string path = "Json/pedidos.json";
    public List<Pedido> Obtener()
    {
        if (File.Exists(path))
        {
            string jsonEnTexto = File.ReadAllText(path);
            if (jsonEnTexto!=null && jsonEnTexto.Length > 5)
            {
                var Listapedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonEnTexto);
                return Listapedidos;    
            }       
        }    
        return new List<Pedido>(); 
    }
    public void Guardar(List<Pedido> Pedidos)
    {
        if (Pedidos!=null)
        {
            string jsonFormat = JsonSerializer.Serialize(Pedidos);
            if(jsonFormat != null && jsonFormat.Length > 5) File.WriteAllText(path, jsonFormat);
        }
        
    }
}