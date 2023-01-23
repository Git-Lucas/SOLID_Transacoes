using SOLID_Transacoes.Domain.Models.Enums;

namespace SOLID_Transacoes.Domain.Models
{
    public class Transacao
    {
        public string Id { get; set; }
        public double Valor { get; set; }
        public int NumeroParcelas { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public List<Parcela> Parcelas { get; set; } = new();

        public void GerarParcelas()
        {
            var valorParcela = Math.Round(Valor / NumeroParcelas, 2);
            var diferenca = Math.Round(Valor - valorParcela * NumeroParcelas, 2);

            for (int numeroParcela = 1; numeroParcela <= NumeroParcelas; numeroParcela++)
            {
                if (numeroParcela == NumeroParcelas)
                    valorParcela += diferenca;

                Parcelas.Add(new Parcela { NumeroParcela = numeroParcela, Valor = valorParcela });
            }
        }

        public override string ToString()
        {
            var result = $"DADOS PRINCIPAIS:\n" +
                         $"Id: {Id} | " +
                         $"Valor: R${Valor} | " +
                         $"Número Parcelas: {NumeroParcelas} | " +
                         $"Método de Pagamento: {MetodoPagamento}\n\n" +
                         $"DADOS PARCELAS:\n";
                         foreach (Parcela p in Parcelas)
                         {
                             result += $"Número Parcela: {p.NumeroParcela} | " +
                                       $"Valor: R${p.Valor}\n";
                         }
 
            result += "\n";

            return result;
        }
    }
}
