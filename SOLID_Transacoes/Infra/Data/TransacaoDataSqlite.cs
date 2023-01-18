using Microsoft.EntityFrameworkCore;
using SOLID_Transacoes.Domain.Data;
using SOLID_Transacoes.Domain.Models;
using SOLID_Transacoes.Infra.EntityFramework;

namespace SOLID_Transacoes.Infra.Data
{
    public class TransacaoDataSqlite : ITransacaoData
    {
        private readonly EfSqliteAdapter _context;

        public TransacaoDataSqlite(EfSqliteAdapter context)
        {
            _context = context;
        }

        public async Task CreateAsync(Transacao transacao)
        {
            var transacaoExistente = await _context.Transacoes.SingleOrDefaultAsync(x => x.Id == transacao.Id);
            if (transacaoExistente is not null)
            {
                _context.Remove(transacaoExistente);
                await _context.SaveChangesAsync();
            }

            _context.Add(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task<Transacao> GetAsync(string id)
        {
            var transacao = await _context.Transacoes.Include(x => x.Parcelas)
                                                     .SingleOrDefaultAsync(x => x.Id == id);

            if (transacao is not null)
                return transacao;
            else
                throw new Exception("Não encontrado.");
        }

        public async Task<List<Transacao>> GetAllAsync() =>
            await _context.Transacoes.Include(x => x.Parcelas).ToListAsync();

        public async Task DeleteAsync(string id)
        {
            //Para a exclusão em cascata no EF 6+, basta "trackear" a tabela da FK no objeto que será
            //removido. Ou seja, ".Include(x => x.Parcelas)"
            var transacao = await _context.Transacoes.Include(x => x.Parcelas)
                                                     .SingleOrDefaultAsync(x => x.Id == id);

            if (transacao is not null)
            {
                _context.Remove(transacao);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Não encontrado.");
            }
        }
    }
}
