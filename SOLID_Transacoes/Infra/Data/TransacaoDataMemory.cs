using SOLID_Transacoes.Domain.Data;
using SOLID_Transacoes.Domain.Models;

namespace SOLID_Transacoes.Infra.Data
{
    public class TransacaoDataMemory : ITransacaoData
    {
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public async Task CreateAsync(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }

        public async Task DeleteAsync(string id)
        {
            var transacao = Transacoes.SingleOrDefault(x => x.Id == id);

            if (transacao is not null)
                Transacoes.Remove(transacao);
            else
                throw new Exception("Não encontrado.");
        }

        public async Task<List<Transacao>> GetAllAsync() =>
            Transacoes.ToList();

        public async Task<Transacao> GetAsync(string id)
        {
            var transacao = Transacoes.SingleOrDefault(x => x.Id == id);

            if (transacao is not null)
                return transacao;
            else
                throw new Exception("Não encontrado.");
        }
    }
}
