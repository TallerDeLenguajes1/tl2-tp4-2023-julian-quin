using System.Text.Json;
using EspacioPedido;
namespace EspacioAccesoADatosPedidos
{
    public class AccesoADatosPedidos
    {
        private const string path ="pedidos.json";
        public List<Pedido> Obtener()
        {
            
            string JsonEnTexto = File.ReadAllText(path);
            var Listapedidos = JsonSerializer.Deserialize<List<Pedido>>(JsonEnTexto);

            return Listapedidos;
        }
        public void Guardar(List<Pedido> Pedidos)
        {
            string JsonFormat = JsonSerializer.Serialize(Pedidos);
            File.WriteAllText(path, JsonFormat);
        }
    }
}