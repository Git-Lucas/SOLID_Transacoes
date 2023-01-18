using SOLID_Transacoes.Domain.Data;
using SOLID_Transacoes.Domain.Models;

namespace SOLID_Transacoes.Application
{
    public class DeletaTransacao
    {
        private readonly ITransacaoData _transacaoData;

        public DeletaTransacao(ITransacaoData transacaoData)
        {
            _transacaoData = transacaoData;
        }

        public async Task ExecutaAsync(string id)
        {
            await _transacaoData.DeleteAsync(id);
        }
    }
}
