using Models;

namespace Data
{
    public interface IUsuariosRepository
    {
        void addUsuario(Usuarios account);
        Usuarios? getUser(int userId);
        Usuarios? getUserByEmail(string _email);
        void modificarUsuario(Usuarios account);
        void guardarCuentas();
    }
}