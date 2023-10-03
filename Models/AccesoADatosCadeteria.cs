using System.Text.Json;
namespace tl2_tp4_2023_julian_quin;
public class AccesoADatosCadeteria
{
    public Cadeteria Obtener()
    {
        const string path = "Json/DatosCadeteria.json";
        if (File.Exists(path))
        {
            string JsonAtexto = File.ReadAllText(path);
            var datosCadeteria = JsonSerializer.Deserialize<Cadeteria>(JsonAtexto);
            return datosCadeteria; 
        }
        return new Cadeteria();  
    }
}
