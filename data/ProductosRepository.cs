using System.Text.Json;
using Models;

namespace Data
{
    public class ProductosRepository : IProductosRepository
    {
        private Dictionary<int, Productos> _products = new Dictionary<int, Productos>();
        private readonly string _filePath = "../../../ddbb/Productos.json";
        

        public ProductosRepository()
        {
            cargarProductos();
        }
        public void addProducto(Productos product)
        {
            _products[product.id] = product;
            guardarProductos();
        }

        public Productos? getProduct(int productId)
        {
            return _products.TryGetValue(productId, out var account) ? account : null;
        }
        public Productos? getProductbyName(string _nombre)
        {
            return _products.FirstOrDefault(x => x.Value.nombre == _nombre.ToLower()).Value ?? null;
        }


        public void modificarProductos(Productos producto)
        {
            _products[producto.id] = producto;
            guardarProductos();
        }

        public void guardarProductos()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string jsonString = JsonSerializer.Serialize(_products.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void cargarProductos()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(_filePath);
                    var productos = JsonSerializer.Deserialize<List<Productos>>(jsonString);
                    _products = productos.ToDictionary(product => product.id);
                }catch{
                    Console.WriteLine("No hay productos");
                }


            }
        }
    }
}
