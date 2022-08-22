using FileHelpers;

namespace TestesClasses
{
    [DelimitedRecord(";")]
    public class CabecalhoArquivoModel
    {
        public string DataHoraArquivo { get; set; }
        public long QtdOperacoes { get; set; }
        public long QtdRegistrosSucesso { get; set; }
        public long QtdRegistrosFalha { get; set; }
    }

    [DelimitedRecord(";")]
    public class InfoOperacaoModel
    {
        public char TipoOperacao { get; set; } = 'O';
        public long NumeroLinha { get; set; }
        public string CodigoSCR { get; set; }
        public string CodigoValidacao { get; set; }
    }

    [DelimitedRecord(";")]
    public class InfoOperacaoErroModel : InfoOperacaoModel { }
}