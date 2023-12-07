using System.Text.Json;
namespace Models;


using System;
using System.IO;

public class Usuarios
{
        //atributes
        private static readonly string _filePath = "../../../ddbb/Usuarios.json";


        public static int seed;
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public int id { get; set; }
        public decimal saldo { get; set; }
        public Dictionary<Productos, int>? mochila { get; set; }
        public List<Mercado>? mercado;



        public Usuarios(string _correo, string _contraseña)
        {
                InicializarSeed();
                correo = _correo;
                password = _contraseña;
                id = seed;
                // seed++;
                saldo = 100;
                nombre = "";
                apellido = "";
                mochila = new Dictionary<Productos, int>() ;
                mercado = new List<Mercado>();
        }
        public Usuarios()
        {
                id = seed;
                saldo = 100;
        }


        private static void InicializarSeed()
        {
                string jsonString = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                        seed = 0;
                }
                else
                {
                        var cuentas = JsonSerializer.Deserialize<List<Usuarios>>(jsonString);
                        seed = cuentas.Count();
                }
        }



}