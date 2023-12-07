namespace Models;

public class Mercado
{
    public Usuarios usuario;
    public Productos producto;
    public int cantidad;
    public string accion;
    public decimal precio;

    public Mercado(Usuarios _user, Productos _product, int _quantity, string _action){
        usuario = _user;
        producto = _product;
        cantidad = _quantity;
        accion = _action;
        precio = _product.precio * _quantity;
    }
}