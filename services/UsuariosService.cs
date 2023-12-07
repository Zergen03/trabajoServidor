using System.Collections;
using System.Reflection.Metadata;
using Data;
using Models;
namespace Services
{
    public class UsuariosService : IUsuariosService
    {

        private readonly IUsuariosRepository _repository;
        private readonly IProductosRepository _productosRepository;

        public UsuariosService(IUsuariosRepository usuariosRepository, IProductosRepository productosRepository)
        {
            _productosRepository = productosRepository;
            _repository = usuariosRepository;
        }



        public void comprar(Productos _product, int _quantity, Usuarios user)
        {
            decimal precioTotal = _product.precio * _quantity;
            if (user.saldo < precioTotal) throw new Exception("No tienes suficiente dinero");
            if (_product.stock < _quantity) throw new Exception("No hay stock");
            user.saldo -= precioTotal;
            _product.stock -= _quantity;
            if (user.mochila.ContainsKey(_product))
            {
                user.mochila[_product] += _quantity;
            }
            else
            {
                user.mochila.Add(_product, _quantity);
            }

            Mercado newMercado = new Mercado(user, _product, _quantity, "compra");
            user.mercado.Add(newMercado);
            _productosRepository.modificarProductos(_product);
            _repository.modificarUsuario(user);
        }
        public void vender(Productos _product, int _quantity, Usuarios user)
        {
            decimal precioTotal = _product.precio * _quantity;
            if (user.mochila[_product] < _quantity) throw new Exception("No tienes suficientes " + _product.nombre);
            user.saldo += precioTotal;
            user.mochila[_product] = user.mochila[_product] - _quantity;
            _product.stock += _quantity;
            int cantidad = user.mochila.TryGetValue(_product, out var quantity) ? quantity : 0;
            Mercado newMercado = new Mercado(user, _product, _quantity, "venta");
            user.mercado.Add(newMercado);
            _productosRepository.modificarProductos(_product);
            _repository.modificarUsuario(user);
        }

        public void actualizarUsuario(int _id, string _name, string _lastName, string _password)
        {
            Usuarios user = _repository.getUser(_id) ?? throw new Exception("No existe este usuario");
            user.nombre = _name;
            user.apellido = _lastName;
            user.password = _password;
            _repository.modificarUsuario(user);
        }

        public Usuarios login(string _email, string _password)
        {
            Usuarios? user = _repository.getUserByEmail(_email);
            if (user == null || user.password != _password) throw new Exception("Usuario o contraseña incorrectos");
            return user;
        }

        public void register(String _correo, string _password)
        {
            if (_repository.getUserByEmail(_correo) == null)
            {
                Usuarios user = new Usuarios(_correo, _password);
                _repository.addUsuario(user);
            }
            else
            {
                if (_correo != "admin@gmail.com")
                {
                    Console.WriteLine("Este correo ya existe");
                }
            }
        }

        public void verMochila(Usuarios _user)
        {
            foreach (var item in _user.mochila)
            {
                Console.WriteLine("-Producto:\n" + item.Key.ToString() + "\ncantidad: " + item.Value);
                Console.WriteLine("----------------------------");
            }
        }
    }
}
