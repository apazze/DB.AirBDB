using DB.AirBDB.Common.Utils.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DB.AirBDB.Services.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var arqXlsxUsuarios = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\usuarios.xlsx";
            var arqJsonUsuarios = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\listaDeUsuarios.json";

            var conversorUsuarios = new GeraListaDeUsuariosJson(arqXlsxUsuarios, arqJsonUsuarios);
            conversorUsuarios.ConversorXLSXParaJson();

            var arqJsonLugaresEntrada = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\lugares.json";
            var arqJsonLugaresSaida = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\listaDeLugares.json";
            var manipuladorLugares = new ManipuladorLugaresJson(arqJsonLugaresEntrada, arqJsonLugaresSaida);
            manipuladorLugares.GeraListaDeLugaresJson();


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
