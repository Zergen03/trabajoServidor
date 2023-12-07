using System.Text.Json;
namespace Models;

public class Productos
{
    //atributes

    private static readonly string _filePath = "../../../ddbb/Productos.json";


    public static int seed;
    public string nombre { get; set; }
    public int id { get; set; }
    public decimal precio { get; set; }
    public int stock { get; set; }
    public string categoria { get; set; }
    public DateTime caducidad { get; set; }
    public Boolean descatalogado { get; set; }

    //constructor
    public Productos(string _nombre, decimal _precio, string _categoria, int _stock)
    {

        InicializarSeed();
        nombre = _nombre;
        precio = _precio;
        categoria = _categoria;
        stock = _stock;
        caducidad = DateTime.Now.AddDays(20);
        id = seed;
    }
    public Productos()
    {
        id = seed;
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
            var products = JsonSerializer.Deserialize<List<Usuarios>>(jsonString);
            seed = products.Count();
        }
    }

    public override string ToString()
    {
        return "nombre: " + nombre + "\n"
        + "precio " + precio + "\n"
        + "categoria " + categoria + "\n"
        + "caducidad " + caducidad;
    }

}