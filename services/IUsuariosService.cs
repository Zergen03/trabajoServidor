using Models;


namespace Services{
    public interface IUsuariosService{
        void comprar(Productos _product, int _quantity, Usuarios user);
        void vender(Productos _product, int _quantity, Usuarios user);
        public void actualizarUsuario(int _id, string _name, string _lastName, string _password);
        Usuarios login(string _email, string _password);
        void register(String _correo, string _password);
        void verMochila(Usuarios _user);

    }

}