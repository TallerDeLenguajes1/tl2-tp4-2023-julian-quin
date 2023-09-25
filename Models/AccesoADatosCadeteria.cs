using System.Text.Json;
using EspacioCadeteria;
namespace EspacioDatosCadeteria
{
    public class AccesoADatosCadeteria
    {
        public Cadeteria Obtener()
        {
            const string path = "DatosCadeteria.json";
            string JsonAtexto = File.ReadAllText(path);
            var datosCadeteria = JsonSerializer.Deserialize<Cadeteria>(JsonAtexto);    
            return datosCadeteria; 
        }
    }
}