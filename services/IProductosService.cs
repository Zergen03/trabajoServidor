using Models;

namespace Services{
    public interface IProductosService{
      public void descatalogar (int _id);
      public void modificarStock (int _id, int _nuevoStock);
      public void crearProducto(string _nombre, decimal _precio, string _categoria, int _stock);
    }

}