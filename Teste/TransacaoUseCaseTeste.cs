namespace Teste
{
    [TestClass]
    public class TransacaoUseCaseTeste
    {
        [TestMethod]
        public async Task CriaUmaTransacaoMemoria()
        {
            var id = new Random().Next(1000, 9999).ToString();
            var transacaoData = new TransacaoDataMemory();
            var criaTransacaoUseCase = new CriaTransacao(transacaoData);
            await criaTransacaoUseCase.ExecutaAsync(id, 1000, 12, MetodoPagamento.CartaoCredito);

            var transacao = await new VisualizaTransacao(transacaoData).ExecutaAsync(id);

            Assert.AreEqual(id, transacao.Id);
            Assert.AreEqual(1000, transacao.Valor);
            Assert.AreEqual(12, transacao.NumeroParcelas);
            Assert.AreEqual(12, transacao.Parcelas.Count);
            Assert.AreEqual(MetodoPagamento.CartaoCredito, transacao.MetodoPagamento);
            Assert.AreEqual(83.33, transacao.Parcelas.First(x => x.NumeroParcela == 1).Valor);
            Assert.AreEqual(83.37, transacao.Parcelas.First(x => x.NumeroParcela == 12).Valor);
        }

        [TestMethod]
        public async Task CriaUmaTransacaoSqlite()
        {
            var id = new Random().Next(1000, 9999).ToString();
            var transacaoData = new TransacaoDataSqlite(new EfSqliteAdapter());
            var criaTransacaoUseCase = new CriaTransacao(transacaoData);
            await criaTransacaoUseCase.ExecutaAsync(id, 1000, 12, MetodoPagamento.CartaoCredito);

            var transacao = await new VisualizaTransacao(transacaoData).ExecutaAsync(id);

            await new DeletaTransacao(transacaoData).ExecutaAsync(id);

            Assert.AreEqual(id, transacao.Id);
            Assert.AreEqual(1000, transacao.Valor);
            Assert.AreEqual(12, transacao.NumeroParcelas);
            Assert.AreEqual(12, transacao.Parcelas.Count);
            Assert.AreEqual(MetodoPagamento.CartaoCredito, transacao.MetodoPagamento);
            Assert.AreEqual(83.33, transacao.Parcelas.First(x => x.NumeroParcela == 1).Valor);
            Assert.AreEqual(83.37, transacao.Parcelas.First(x => x.NumeroParcela == 12).Valor);
        }
    }
}
