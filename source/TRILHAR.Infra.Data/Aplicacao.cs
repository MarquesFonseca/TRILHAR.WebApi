using Microsoft.Extensions.Configuration;
using System.IO;

namespace TRILHAR.Infra.Data
{
    public class Aplicacao
    {
        public Aplicacao()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //Ano = configuration.GetSection("Aplicacao:Ano").Value;
            //EndpointRfb = configuration.GetSection("Aplicacao:EndpointRfb").Value;
            //JasperReportServer = configuration.GetSection("Aplicacao:JasperReportServer").Value;
            //DbLinkServer = configuration.GetSection("Aplicacao:dbLinkServer").Value;
        }

        public string Ano { get; set; }
        public string EndpointRfb { get; set; }
        public string JasperReportServer { get; set; }
        public string DbLinkServer { get; set; }
        public string IdentityServer { get; set; }
        public string Sistema { get; set; }
        public bool Producao { get; set; }
        public bool PathSwaggerFull { get; set; }
        public string ApiTexudoUrl { get; set; }
        public string[] UrlsLiberadas
        {
            get
            {
                if (Producao)
                {
                    return new string[] {
                        //""
                    };
                }
                return new string[] {
                    "http://localhost:4200"
                    ,"http://localhost"
                };
            }
        }
    }
}