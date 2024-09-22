using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TRILHAR.Infra.Data.Config
{
    public class AplicacaoConfig
    {
        private static readonly IConfiguration _configuracoes;

        static AplicacaoConfig()
        {
            _configuracoes = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }
        public static string ObterConfiguracao(string chave)
        {
            return _configuracoes[chave];
        }
        public static string ObterAno()
        {
            return _configuracoes["Aplicacao:Ano"];
        }

    }
}
