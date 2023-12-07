using System.Collections;
using System.Reflection.Metadata;
using Data;
using Models;
namespace Services
{

    public class ProductService : IProductosService
    {

        private readonly IProductosRepository _repository;

        public ProductService(IProductosRepository productsRepository)
        {
            _repository = productsRepository;
        }



        public void descatalogar(int _id)
        {
            Productos product = _repository.getProduct(_id);
            product.descatalogado = false;
            _repository.modificarProductos(product);
        }

        public void modificarStock(int _id, int _nuevoStock)
        {
            Productos product = _repository.getProduct(_id);
            product.stock = _nuevoStock;
            _repository.modificarProductos(product);
        }

        public void crearProducto(string _nombre, decimal _precio, string _categoria, int _stock)
        {
            Productos product = new Productos(_nombre, _precio, _categoria, _stock);
            _repository.addProducto(product);
        }



    }
}