using System.Text.Json;
namespace tl2_tp4_2023_julian_quin;
public class AccesoADatosCadeteria
{
    private const string path = "Json/DatosCadeteria.json";
    public Cadeteria Obtener()
    {
        
        if (File.Exists(path))
        {
            string jsonAtexto = File.ReadAllText(path);
            if (jsonAtexto!=null && jsonAtexto.Length > 5)
            {
                var datosCadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonAtexto);
                return datosCadeteria;  
            }
           
             
        }
        return new Cadeteria();  
    }
}
