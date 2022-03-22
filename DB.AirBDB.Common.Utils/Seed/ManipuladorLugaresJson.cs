using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DB.AirBDB.Common.Utils.Seed
{
    public class ManipuladorLugaresJson
    {
        public string ArquivoEntradaJSON { get; set; } = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\lugares.json";
        public string ArquivoSaidaJSON { get; set; } = "..\\DB.AirBDB.Common.Utils\\Seed\\Dados\\listaDeLugares.json";
        public ManipuladorLugaresJson(string entrada, string saida)
        {
            ArquivoEntradaJSON = entrada;
            ArquivoSaidaJSON = saida;
        }

        public void GeraListaDeLugaresJson()
        {
            string jsonString = File.ReadAllText(ArquivoEntradaJSON);
            List<LugaresModel> origem = JsonConvert.DeserializeObject<List<LugaresModel>>(jsonString);
            List<string> listaDescricao = new List<string>();
            List<string> listaCidade = new List<string>();
            List<string> listaTiposDeAcomodacao = new List<string>();
            List<decimal> listaValor = new List<decimal>();
            string[] tiposDeAcomodacaoPermitidos = { "Casa", "Apartamento", "Quarto" };
            Random random = new Random();

            foreach (var item in origem)
            {
                string descricao = item.logradouro + " "
                        + item.numero + " "
                        + item.complemento + " "
                        + item.bairro + " "
                        + item.estadoSigla;

                listaDescricao.Add(descricao);
                listaCidade.Add(item.cidade);
                var tipoAcomodacao = tiposDeAcomodacaoPermitidos[random.Next(tiposDeAcomodacaoPermitidos.Length)];
                listaTiposDeAcomodacao.Add(tipoAcomodacao);
                var valor = random.Next(100, 1000);
                listaValor.Add(valor);
            }

            List<LugaresJson> listaJson = new List<LugaresJson>();

            for (int i = 0; i < origem.Count; i++)
            {
                var obj = new LugaresJson();
                obj.Descricao = listaDescricao[i];
                obj.Cidade = listaCidade[i];
                obj.TipoDeAcomodacao = listaTiposDeAcomodacao[i];
                obj.Valor = listaValor[i];
                listaJson.Add(obj);
            }

            EscreveArquivoSaida(listaJson);
        }

        private void EscreveArquivoSaida(List<LugaresJson> listaJson)
        {
            using (var outFile = File.CreateText(ArquivoSaidaJSON))
            {
                using (var writer = new JsonTextWriter(outFile))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartArray();

                    foreach (var item in listaJson)
                    {
                        writer.WriteStartObject();

                        writer.WritePropertyName("Descricao");
                        writer.WriteValue(item.Descricao);

                        writer.WritePropertyName("TipoDeAcomodacao");
                        writer.WriteValue(item.TipoDeAcomodacao);

                        writer.WritePropertyName("Cidade");
                        writer.WriteValue(item.Cidade);

                        writer.WritePropertyName("Valor");
                        writer.WriteValue(item.Valor);

                        writer.WriteEndObject();
                    }

                    writer.WriteEndArray();
                }
            }
        }
    }

    internal class LugaresModel
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string estadoSigla { get; set; }
    }

    public class LugaresJson
    {
        public string Descricao { get; set; }
        public string TipoDeAcomodacao { get; set; }
        public string Cidade { get; set; }
        public decimal Valor { get; set; }
    }
}
