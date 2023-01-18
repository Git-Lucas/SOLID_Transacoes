using SOLID_Transacoes.Domain.Models;

namespace SOLID_Transacoes.Domain.Data
{
    public interface ITransacaoData
    {
        Task CreateAsync(Transacao transacao);

        Task<Transacao> GetAsync(string id);

        Task<List<Transacao>> GetAllAsync();

        Task DeleteAsync(string id);
    }
}
