using Data;
using Models;
using Services;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<IUsuariosService, UsuariosService>();
serviceCollection.AddTransient<IProductosService, ProductService>();
serviceCollection.AddSingleton<IUsuariosRepository, UsuariosRepository>();
serviceCollection.AddSingleton<IProductosRepository, ProductosRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();
var productosRepository = serviceProvider.GetService<IProductosRepository>();
var productosService = serviceProvider.GetService<IProductosService>();
var usuariosService = serviceProvider.GetService<IUsuariosService>();

usuariosService.register("admin@gmail.com", "admin");
Menu.menu();