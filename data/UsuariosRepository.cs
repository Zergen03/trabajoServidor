using System.Text.Json;
using Models;

namespace Data
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private Dictionary<int, Usuarios> _users = new Dictionary<int, Usuarios>();
        private readonly IProductosRepository _productosRepository;
        private readonly string _filePath = "../../../ddbb/Usuarios.json";

        public UsuariosRepository(IProductosRepository productosRepository)
        {
            _productosRepository = productosRepository;
            cargarCuentas();
        }
        public void addUsuario(Usuarios account)
        {
            _users[account.id] = account;
            guardarCuentas();
        }

        public Dictionary<Productos, int> verMochila(Usuarios _user)
        {
            return _user.mochila;
        }

        public Usuarios? getUser(int userId)
        {
            return _users.TryGetValue(userId, out var account) ? account : null;
        }
        public Usuarios? getUserByEmail(string _email)
        {
            return _users.FirstOrDefault(x => x.Value.correo == _email).Value ?? null;
        }

        public void modificarUsuario(Usuarios account)
        {
            _users[account.id] = account;
            guardarCuentas();
        }

        public void guardarCuentas()
        {

            var usuariosParaSerializar = _users.Values.Select(usuario =>
               new
               {
                   usuario.nombre,
                   usuario.apellido,
                   usuario.correo,
                   usuario.password,
                   usuario.id,
                   usuario.saldo,
                   Mochila = usuario.mochila?.ToDictionary(item => item.Key.id, item => item.Value),
                   Mercado = usuario.mercado?.Select(item =>
                        new
                        {
                            UsuarioId = item.usuario.id,
                            ProductoId = item.producto.id,
                            item.cantidad,
                            item.accion,
                            item.precio
                        }
                    ).ToList()
               }
           ).ToList();

            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string jsonString = JsonSerializer.Serialize(usuariosParaSerializar, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void cargarCuentas()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(_filePath);
                    using JsonDocument doc = JsonDocument.Parse(jsonString);
                    JsonElement root = doc.RootElement;

                    var cuentasDeserializadas = root.EnumerateArray();

                    _users = new Dictionary<int, Usuarios>();

                    foreach (JsonElement cuentaElement in cuentasDeserializadas)
                    {

                        var usuario = new Usuarios
                        {
                            nombre = cuentaElement.GetProperty("nombre").GetString(),
                            apellido = cuentaElement.GetProperty("apellido").GetString(),
                            correo = cuentaElement.GetProperty("correo").GetString(),
                            password = cuentaElement.GetProperty("password").GetString(),
                            id = cuentaElement.GetProperty("id").GetInt32(),
                            saldo = cuentaElement.GetProperty("saldo").GetDecimal(),
                            mochila = new Dictionary<Productos, int>(),
                            mercado = new List<Mercado>()
                        };

                        // Recoger datos de la mochila
                        if (cuentaElement.TryGetProperty("Mochila", out JsonElement mochilaElement))
                        {
                            foreach (JsonProperty item in mochilaElement.EnumerateObject())
                            {
                                int productId = int.Parse(item.Name);
                                int cantidad = item.Value.GetInt32();
                                var producto = _productosRepository.getProduct(productId);
                                if (producto != null)
                                {
                                    usuario.mochila.Add(producto, cantidad);
                                }
                            }
                        }

                        // Recoger datos del mercado
                        if (cuentaElement.TryGetProperty("Mercado", out JsonElement mercadoElement))
                        {
                            foreach (JsonElement item in mercadoElement.EnumerateArray())
                            {
                                int usuarioId = item.GetProperty("UsuarioId").GetInt32();
                                int productoId = item.GetProperty("ProductoId").GetInt32();
                                int cantidad = item.GetProperty("cantidad").GetInt32();
                                string accion = item.GetProperty("accion").GetString();
                                decimal precio = item.GetProperty("precio").GetDecimal();

                                var producto = _productosRepository.getProduct(productoId);
                                if (producto != null)
                                {
                                    var mercado = new Mercado(usuario, producto, cantidad, accion);
                                    usuario.mercado.Add(mercado);
                                }
                            }
                        }

                        _users.Add(usuario.id, usuario);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar cuentas: {ex.Message}");
                }
            }
        }

    }
}
