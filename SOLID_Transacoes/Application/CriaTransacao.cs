using SOLID_Transacoes.Domain.Data;
using SOLID_Transacoes.Domain.Models;
using SOLID_Transacoes.Domain.Models.Enums;

namespace SOLID_Transacoes.Application
{
    public class CriaTransacao
    {
        private readonly ITransacaoData _transacaoData;

        public CriaTransacao(ITransacaoData transacaoData)
        {
            _transacaoData = transacaoData;
        }

        public async Task<Transacao> ExecutaAsync(string id, double valor, int numeroParcelas, MetodoPagamento metodoPagamento)
        {
            var transacao = new Transacao
            {
                Id = id,
                Valor = valor,
                NumeroParcelas = numeroParcelas,
                MetodoPagamento = metodoPagamento
            };

            transacao.GeraParcelas();

            await _transacaoData.CreateAsync(transacao);

            return transacao;
        }
    }
}
