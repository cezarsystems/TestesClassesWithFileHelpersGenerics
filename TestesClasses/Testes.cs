using System.Text;
using FileHelpers;

namespace TestesClasses
{
    class Testes
    {
        static void Main()
        {
            Console.WriteLine("Teste!");

            CabecalhoArquivoModel cabecalhoArquivo = new()
            {
                DataHoraArquivo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                QtdOperacoes = 10,
                QtdRegistrosFalha = 50,
                QtdRegistrosSucesso = 50
            };

            var listaDados = new List<InfoOperacaoModel>()
            {
                new() { CodigoSCR = "00000001", CodigoValidacao = "1001", NumeroLinha = 1, TipoOperacao = 'A' },
                new() { CodigoSCR = "00000002", CodigoValidacao = "1002", NumeroLinha = 1, TipoOperacao = 'B' },
                new() { CodigoSCR = "00000003", CodigoValidacao = "1003", NumeroLinha = 1, TipoOperacao = 'C' },
                new() { CodigoSCR = "00000004", CodigoValidacao = "1004", NumeroLinha = 1, TipoOperacao = 'D' },
                new() { CodigoSCR = "00000005", CodigoValidacao = "1005", NumeroLinha = 1, TipoOperacao = 'E' }
            };

            var linhaCabecalho = GetContentString<CabecalhoArquivoModel>(cabecalhoArquivo);

            var linhasInfoOperacao = GetContentString<InfoOperacaoModel>(listaDados);

            File.WriteAllLines(@"D:\testes.txt", new[] {
                RemoveLastNewLineFromString(linhaCabecalho),
                RemoveLastNewLineFromString(linhasInfoOperacao)
            }, Encoding.UTF8);

            Console.ReadLine();
        }

        private static string GetContentString<T>(object obj)
        {
            if (obj is null)
                throw new Exception("O objeto recebido é nulo");

            string modelName = typeof(T).Name;

            string? content = modelName switch
            {
                "CabecalhoArquivoModel" => new 
                    FileHelperEngine<CabecalhoArquivoModel>()
                        .WriteString(ConvertObjectToIEnumerable<CabecalhoArquivoModel>(obj)),
                "InfoOperacaoModel" => new
                    FileHelperEngine<InfoOperacaoModel>()
                        .WriteString(ConvertObjectToIEnumerable<InfoOperacaoModel>(obj)),
                _ => null
            };

            return !string.IsNullOrEmpty(content) ? content : throw new Exception("Não foi possível converter os dados do objeto para texto utilizando a model " + modelName);
        }

        private static IEnumerable<T> ConvertObjectToIEnumerable<T>(object objValue)
        {
            var objects = new[] { objValue };

            if (objects?.Length > 0)
            {
                foreach (var obj in objects)
                {
                    if (obj.GetType().Name.Equals(typeof(T).Name))
                        yield return (T)obj;
                    else
                    {
                        var newObjectList = new { obj };

                        foreach (var newObjectItem in (IEnumerable<T>)newObjectList.obj)
                            yield return newObjectItem;
                    }
                }                    
            }                
        }

        private static string RemoveLastNewLineFromString(string stringContent)
        {
            if (stringContent?.Length >= 2)
                return stringContent.Remove(stringContent.Length - 2);
            else
                throw new Exception($"Erro ao remover o retorno de carro e quebra de linha do conteúdo da string { stringContent }");
        }
    }
}