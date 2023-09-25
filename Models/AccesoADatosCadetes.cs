using System.Text.Json;
using EspacioCadete;
namespace EspacioDatosCadetes
{
    public class AccesoADatosCadetes
    {
        public List<Cadete> Obtener()
        {
            const string path="DatosCadetes.json"; 
            List<Cadete> Cadetes;
            string JsonAtexto = File.ReadAllText(path);
            Cadetes = JsonSerializer.Deserialize<List<Cadete>>(JsonAtexto); 
            return Cadetes; 
            
        }
    }
}