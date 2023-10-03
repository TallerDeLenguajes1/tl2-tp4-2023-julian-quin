using System.Text.Json;

namespace tl2_tp4_2023_julian_quin;
public class AccesoADatosCadetes
{
    public List<Cadete> Obtener()
    {
        const string path = "Json/DatosCadetes.json";
        if (File.Exists(path))
        {
            List<Cadete> Cadetes;
            string JsonAtexto = File.ReadAllText(path);
            Cadetes = JsonSerializer.Deserialize<List<Cadete>>(JsonAtexto);
            return Cadetes;    
        }
        return new List<Cadete>();
        
    }
}
