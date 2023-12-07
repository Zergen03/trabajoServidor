using Models;

namespace Data
{
    public interface IProductosRepository
    {
        public void addProducto(Productos product);
        public Productos? getProduct(int productId);
        public Productos? getProductbyName(string _nombre);
        public void modificarProductos(Productos producto);
        public void guardarProductos();
    }
}