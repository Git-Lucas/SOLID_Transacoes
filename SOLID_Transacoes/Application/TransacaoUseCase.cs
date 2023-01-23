using SOLID_Transacoes.Domain.Data;
using SOLID_Transacoes.Domain.Models.Enums;
using SOLID_Transacoes.Domain.Models;

namespace SOLID_Transacoes.Application
{
    public class TransacaoUseCase
    {
        private readonly ITransacaoData _transacaoData;

        public TransacaoUseCase(ITransacaoData transacaoData)
        {
            _transacaoData = transacaoData;
        }

        public async Task<Transacao> CriarAsync(string id, double valor, int numeroParcelas, MetodoPagamento metodoPagamento)
        {
            var transacao = new Transacao
            {
                Id = id,
                Valor = valor,
                NumeroParcelas = numeroParcelas,
                MetodoPagamento = metodoPagamento
            };

            transacao.GerarParcelas();

            await _transacaoData.CreateAsync(transacao);

            return transacao;
        }

        public async Task DeletarAsync(string id)
        {
            await _transacaoData.DeleteAsync(id);
        }

        public async Task<Transacao> VisualizarPorIdAsync(string id) =>
            await _transacaoData.GetAsync(id);

        public async Task<List<Transacao>> VisualizarTodasAsync() =>
            await _transacaoData.GetAllAsync();
    }
}
