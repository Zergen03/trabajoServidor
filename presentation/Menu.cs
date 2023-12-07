using Data;

using Services;
using Microsoft.Extensions.DependencyInjection;




namespace Models
{
    public class Menu
    {

        // public static int presentation()
        // {
        //     Console.WriteLine("Inventory manager\r");
        //     Console.WriteLine("1) Login");
        //     Console.WriteLine("2) Register");
        //     Console.WriteLine("------------------------\n");
        //     int res = Convert.ToInt32(Console.ReadLine());
        //     return res;
        // }

        // public Usuarios login()
        // {
        //     string[] login = new string[2];
        //     Console.WriteLine("mail:");
        //     login[0] = Console.ReadLine() ?? throw new Exception("el email no es valido");
        //     Console.WriteLine("password:");
        //     login[1] = Console.ReadLine() ?? throw new Exception("la contraseña no es valida");
        //     var user = _usuariosService.login(login[0], login[1]);
        //     return user;
        // }

        // public static string main()
        // {
        //     Console.WriteLine("Que quieres hacer?");
        //     Console.WriteLine("añadir dinero (0)");
        //     Console.WriteLine("sacar dinero(1)");
        //     Console.WriteLine("ver listado de operaciones(2)");
        //     Console.WriteLine("volver atras(any)");
        //     return Console.ReadLine() ?? "3";
        // }


        public static void menu()
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IUsuariosService, UsuariosService>();
            serviceCollection.AddTransient<IProductosService, ProductService>();
            serviceCollection.AddSingleton<IUsuariosRepository, UsuariosRepository>();
            serviceCollection.AddSingleton<IProductosRepository, ProductosRepository>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var usuariosService = serviceProvider.GetService<IUsuariosService>();
            var productosRepository = serviceProvider.GetService<IProductosRepository>();
            var productosService = serviceProvider.GetService<IProductosService>();

            int res = 0;
            do
            {
                Console.WriteLine("Inventory manager\r");
                Console.WriteLine("1) Login");
                Console.WriteLine("2) Register");
                Console.WriteLine("0) Salir");
                Console.WriteLine("------------------------\n");
                res = Convert.ToInt32(Console.ReadLine());
                    switch (res)
                    {
                    case 1:
                        string[] login = new string[2];
                        Console.WriteLine("mail:");
                        login[0] = Console.ReadLine() ?? throw new Exception("el email no es valido");
                        Console.WriteLine("password:");
                        login[1] = Console.ReadLine() ?? throw new Exception("la contraseña no es valida");
                        Usuarios actualUser = usuariosService.login(login[0], login[1]);
                        int res2 = 0;
                        do
                        {
                            if (actualUser.correo == "admin@gmail.com" && actualUser.password == "admin")
                        {
                            Console.WriteLine("-----------------------------------------------------\n");
                            Console.WriteLine();
                            Console.WriteLine("Que quieres hacer ahora:");
                            Console.WriteLine("1) Comprar");
                            Console.WriteLine("2) Vender");
                            Console.WriteLine("3) Modificar Usuario");
                            Console.WriteLine("4) Crear Producto");
                            Console.WriteLine("5) Deslogearte");
                             res2 = Convert.ToInt32(Console.ReadLine());
                            switch (res2)
                            {
                                case 1:
                                    Console.WriteLine("Dime que quieres comprar:");
                                    string _nombre = Console.ReadLine();
                                    Console.WriteLine("Dime la cantidad que quieres comprar:");
                                    int _quantity = Convert.ToInt32(Console.ReadLine());
                                    usuariosService.comprar(productosRepository.getProductbyName(_nombre), _quantity, actualUser);
                                    break;
                                case 2:
                                    Console.WriteLine("Dime que quieres vender:");
                                    _nombre = Console.ReadLine();
                                    Console.WriteLine("Dime la cantidad que quieres vender:");
                                    _quantity = Convert.ToInt32(Console.ReadLine());
                                    usuariosService.vender(productosRepository.getProductbyName(_nombre), _quantity, actualUser);
                                    break;
                                case 3:
                                    int id = actualUser.id;
                                    Console.WriteLine("Dime tu nombre");
                                    _nombre = Console.ReadLine();
                                    Console.WriteLine("Dime tu apellido");
                                    string _apellido = Console.ReadLine();
                                    Console.WriteLine("Dime una nueva contrasena");
                                    string _contrasena = Console.ReadLine();
                                    usuariosService.actualizarUsuario(id, _nombre, _apellido, _contrasena);
                                    break;
                                case 4:
                                    Console.WriteLine("Dime el nombre del producto");
                                    _nombre = Console.ReadLine();
                                    Console.WriteLine("Dime el precio  del producto");
                                    decimal _precio = Convert.ToDecimal(Console.ReadLine());
                                    Console.WriteLine("Dime la categoria del producto");
                                    string _categoria = Console.ReadLine();
                                    Console.WriteLine("Dime la cantidad de stock del producto");
                                    int _stock = Convert.ToInt32(Console.ReadLine());

                                    productosService.crearProducto(_nombre, _precio, _categoria, _stock);

                                    break;
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("-----------------------------------------------------\n");
                            Console.WriteLine();
                            Console.WriteLine("Que quieres hacer ahora:");
                            Console.WriteLine("1) Comprar");
                            Console.WriteLine("2) Vender");
                            Console.WriteLine("3) Modificar Usuario");
                            Console.WriteLine("4) Ver mochila");
                            Console.WriteLine("5) Deslogearte");

                            res2 = Convert.ToInt32(Console.ReadLine());
                            switch (res2)
                            {
                                case 1:
                                    Console.WriteLine("Dime que quieres comprar:");
                                    string _nombre = Console.ReadLine();
                                    Console.WriteLine("Dime la cantidad que quieres comprar:");
                                    int _quantity = Convert.ToInt32(Console.ReadLine());
                                    usuariosService.comprar(productosRepository.getProductbyName(_nombre), _quantity, actualUser);
                                    break;
                                case 2:
                                    Console.WriteLine("Dime que quieres vender:");
                                    _nombre = Console.ReadLine();
                                    Console.WriteLine("Dime la cantidad que quieres vender:");
                                    _quantity = Convert.ToInt32(Console.ReadLine());
                                    usuariosService.vender(productosRepository.getProductbyName(_nombre), _quantity, actualUser);
                                    break;
                                case 3:
                                    int id = actualUser.id;
                                    Console.WriteLine("Dime tu nombre");
                                    _nombre = Console.ReadLine();
                                    Console.WriteLine("Dime tu apellido");
                                    string _apellido = Console.ReadLine();
                                    Console.WriteLine("Dime una nueva contrasena");
                                    string _contrasena = Console.ReadLine();
                                    usuariosService.actualizarUsuario(id, _nombre, _apellido, _contrasena);
                                    break;
                                case 4:
                                    usuariosService.verMochila(actualUser);
                                    break;
                            }
                        }
                        } while (res2 !=5); 
                        break;
                    case 2:
                        //Register
                        Console.WriteLine("Dime tu mail:");
                        string mail = Console.ReadLine();
                        Console.WriteLine("Crea tu contraseña:");
                        string contraseña = Console.ReadLine();
                        usuariosService.register(mail, contraseña);
                        break;

                }

            } while (res != 0);



        }
    }
}
