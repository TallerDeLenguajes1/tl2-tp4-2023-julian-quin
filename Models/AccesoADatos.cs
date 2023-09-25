// using EspacioCadete;
// using EspacioCadeteria;
// using System.Text.Json;

// namespace EspacioAcceso
// {
//     public abstract class AccesoADatos
//     {
//         public abstract List<Cadete> ObtenerCadetes(string path);
//         public abstract Cadeteria InfoCadeteria(string path);

//         public bool PathExist(string path)
//         {
//             if (File.Exists(path)) return true;
//             return false;
//         }

//     }
//     public class AccesoADatos_Csv : AccesoADatos
//     {
//         public override List<Cadete> ObtenerCadetes(string path)
//         {
//             List<Cadete> Cadetes = new();
//             List<string> LineasDelArchivoCsv = File.ReadAllLines(path).ToList();
//             foreach (var linea in LineasDelArchivoCsv)
//             {
//                 string[] SeparacionLineaCsv = linea.Split(",");
//                 //                              //         id            ///  NombreCadete    //   DireccionCadete   //    TelefonoCadete    
//                 var cadete = new Cadete(int.Parse(SeparacionLineaCsv[0]), SeparacionLineaCsv[1], SeparacionLineaCsv[2], SeparacionLineaCsv[3]);
//                 Cadetes.Add(cadete);
//             }
//             return Cadetes;

//         }
//         public override Cadeteria InfoCadeteria(string path)
//         {
//             List<string> LineaDelArchivoCsv = File.ReadAllLines(path).ToList();
//             var DatosCadeteria = LineaDelArchivoCsv[0].Split(",").ToList();
//             return new Cadeteria(DatosCadeteria[0], DatosCadeteria[1], new List<Cadete>());
//         }

//     }
//     public class AccesoADatos_Json : AccesoADatos
//     {
//         public override List<Cadete> ObtenerCadetes(string path) //llega la direccion de un json
//         {

//             List<Cadete> Cadetes;
//             string JsonAtexto = File.ReadAllText(path); // dejo el json en string
//             Cadetes = JsonSerializer.Deserialize<List<Cadete>>(JsonAtexto); //utiliza el constructor
//             return Cadetes; // retorno la lista recuperada
//         }

//         public override Cadeteria InfoCadeteria(string path) //llega la direccion de un json
//         {
//             string JsonAtexto = File.ReadAllText(path);
//             var datosCadeteria = JsonSerializer.Deserialize<Cadeteria>(JsonAtexto);    
//             return datosCadeteria; 
//         }
//     }

// }