using System.Text.Json;

namespace tl2_tp4_2023_julian_quin;
public class AccesoADatosCadetes
{
    private const string path = "Json/DatosCadetes.json";
    public List<Cadete> ObtenerCadetes()
    {
        
        if (File.Exists(path))
        {
            List<Cadete> Cadetes;
            string jsonATexto = File.ReadAllText(path);
            if (jsonATexto!=null && jsonATexto.Length > 5 )
            {
                Cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonATexto);
                return Cadetes;    
            }
               
        }
        return new List<Cadete>();  
    }

    public void GuardarCadetes( List<Cadete> cadetes)
    {
        var jsonTexto = JsonSerializer.Serialize(cadetes);
        if(jsonTexto!=null && jsonTexto.Length > 5) File.WriteAllText(path,jsonTexto);
    }
}
