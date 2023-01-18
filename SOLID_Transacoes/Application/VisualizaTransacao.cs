using SOLID_Transacoes.Domain.Data;
using SOLID_Transacoes.Domain.Models;

namespace SOLID_Transacoes.Application
{
    public class VisualizaTransacao
    {
        private readonly ITransacaoData _transacaoData;

        public VisualizaTransacao(ITransacaoData transacaoData)
        {
            _transacaoData = transacaoData;
        }

        public async Task<Transacao> ExecutaAsync(string id) =>
            await _transacaoData.GetAsync(id);

        public async Task<List<Transacao>> ExecutaAsync() =>
            await _transacaoData.GetAllAsync();
    }
}
