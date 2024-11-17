using Dapper;
using System.Text.Json;
using TRILHAR.Business.IO.Paginacao;

namespace Trilhar.Forms.Repository.Extensions
{
    public static class RepositoryExtension
    {
        /// <summary>
        /// Tratamento da pagaginação quando todos for false
        /// </summary>
        /// <param name="parametros">Informe os parametros condicionais</param>
        /// <param name="isPaginacao">Informe se exibir todos. Se marcar 'False', então atribuirá a paginação</param>
        /// <param name="page">Informe a Página atual</param>
        /// <param name="pageSize">Informe a Quantidade de registros por página.</param>
        /// <param name="stringSql">Informe a stringSql</param>
        public static void TratamentoPaginacao(ref string stringSql, ref object parametros, bool isPaginacao = false, int page = 1, int pageSize = 10)
        {
            page = page == 0 ? 1 : page;
            int skip = (page - 1) * pageSize;
            if (isPaginacao)
            {
                stringSql += $"OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";
                var parametrosComPaginacao = new DynamicParameters(parametros);
                parametrosComPaginacao.Add("Skip", skip);
                parametrosComPaginacao.Add("Take", pageSize);

                parametros = parametrosComPaginacao;
            }
        }

        /// <summary>
        /// Tratamento da pagaginação quando todos for false
        /// </summary>
        /// <param name="parametros">Informe os parametros condicionais</param>
        /// <param name="isPaginacao">Informe se exibir todos. Se marcar 'False', então atribuirá a paginação</param>
        /// <param name="page">Informe a Página atual</param>
        /// <param name="pageSize">Informe a Quantidade de registros por página.</param>
        /// <param name="stringSql">Informe a stringSql</param>
        public static void TratamentoPaginacao(ref string stringSql, ref DynamicParameters parametros, bool isPaginacao = false, int page = 1, int pageSize = 10)
        {
            if (isPaginacao)
            {
                page = page == 0 ? 1 : page;
                int skip = (page - 1) * pageSize;

                stringSql += $" OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";
                var parametrosComPaginacao = new DynamicParameters(parametros);
                parametrosComPaginacao.Add("Skip", skip);
                parametrosComPaginacao.Add("Take", pageSize);

                parametros = parametrosComPaginacao;
            }
        }

        /// <summary>
        /// Converte os parâmetros recebidos do frontend para um formato compatível com o Dapper.
        /// </summary>
        /// <param name="parametros">Dicionário de parâmetros do tipo string e valor genérico.</param>
        /// <returns>Objeto DynamicParameters com os valores convertidos.</returns>
        public static DynamicParameters ConverterParaParametrosDapper(Dictionary<string, object?> parametros)
        {
            var dynamicParameters = new DynamicParameters();

            if (parametros == null || parametros.Count == 0)
                return dynamicParameters;

            foreach (var parametro in parametros)
            {
                object? valor = parametro.Value;

                if (valor is JsonElement jsonElement)
                {
                    // Converter o JsonElement para o tipo adequado
                    switch (jsonElement.ValueKind)
                    {
                        case JsonValueKind.String:
                            var stringValue = jsonElement.GetString();
                            if (DateTime.TryParse(stringValue, out var dateValue))
                            {
                                valor = dateValue; // Converte para DateTime se for uma data válida
                            }
                            else
                            {
                                valor = stringValue; // Caso contrário, mantém como string
                            }
                            break;

                        case JsonValueKind.Number:
                            if (jsonElement.TryGetInt64(out var longValue))
                            {
                                valor = longValue; // Converte para inteiro
                            }
                            else if (jsonElement.TryGetDecimal(out var decimalValue))
                            {
                                valor = decimalValue; // Converte para decimal
                            }
                            break;

                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            valor = jsonElement.GetBoolean(); // Converte para booleano
                            break;

                        case JsonValueKind.Null:
                            valor = null; // Trata valores nulos
                            break;

                        default:
                            throw new Exception($"Tipo de parâmetro não suportado: {jsonElement.ValueKind}");
                    }
                }

                // Adiciona ao objeto DynamicParameters
                dynamicParameters.Add(parametro.Key, valor);
            }

            return dynamicParameters;
        }
    }
}
