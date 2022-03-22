using System;
using System.IO;
using ExcelDataReader;
using System.Text;
using Newtonsoft.Json;

namespace DB.AirBDB.Common.Utils.Seed
{
    public class GeraListaDeUsuariosJson
    {
        public string ArquivoEntradaXLSX { get; set; } = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\usuarios.xlsx";
        public string ArquivoSaidaJSON { get; set; } = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\listaDeUsuarios.json";
        
        public GeraListaDeUsuariosJson(string origem, string destino)
        {
            ArquivoEntradaXLSX = origem;
            ArquivoSaidaJSON = destino;
        }

        public void ConversorXLSXParaJson()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var inFile = File.Open(ArquivoEntradaXLSX, FileMode.Open, FileAccess.Read))
            using (var outFile = File.CreateText(ArquivoSaidaJSON))
            {
                using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration()
                { FallbackEncoding = Encoding.GetEncoding(1252) }))
                using (var writer = new JsonTextWriter(outFile))
                {
                    writer.Formatting = Formatting.Indented; //I likes it tidy
                    writer.WriteStartArray();
                    reader.Read(); //SKIP FIRST ROW, it's TITLES.
                    do
                    {
                        while (reader.Read())
                        {
                            var nome = reader.GetString(0);
                            string login = GeraLogin(nome);
                            string senha = GeraSenha();

                            //peek ahead? Bail before we start anything so we don't get an empty object
                            if (string.IsNullOrEmpty(nome)) break;

                            writer.WriteStartObject();

                            writer.WritePropertyName("Login");
                            writer.WriteValue(login);

                            writer.WritePropertyName("Senha");
                            writer.WriteValue(senha);

                            writer.WritePropertyName("Nome");
                            writer.WriteValue(nome);

                            writer.WriteEndObject();
                        }
                    } while (reader.NextResult());
                    writer.WriteEndArray();
                }
            }
        }

        private static string GeraSenha()
        {
            var tamanhoDeSenha = 8;
            string valido = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ1234567890@#$%&";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

            while (0 < tamanhoDeSenha--)
            {
                stringBuilder.Append(valido[random.Next(valido.Length)]);
            }
            var senha = stringBuilder.ToString();
            return senha;
        }

        private static string GeraLogin(string nome)
        {
            var nomeSplitado = nome.ToLower().Split(' ');
            var login = nomeSplitado[0] + "_" + nomeSplitado[nomeSplitado.Length - 1];
            return login;
        }
    }
}
