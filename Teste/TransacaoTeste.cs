namespace Teste
{
    [TestClass]
    public class TransacaoTeste
    {
        [TestMethod]
        public void CriaUmaTransacao()
        {
            var transacao = new Transacao
            {
                Valor = 1000,
                NumeroParcelas = 12,
                MetodoPagamento = MetodoPagamento.CartaoCredito
            };

            transacao.GerarParcelas();

            //Count sem o par?nteses, porque n?o ? necess?ria a express?o LINQ
            Assert.AreEqual(12, transacao.Parcelas.Count);
            Assert.AreEqual(83.33, transacao.Parcelas.First(x => x.NumeroParcela == 1).Valor);
            Assert.AreEqual(83.37, transacao.Parcelas.First(x => x.NumeroParcela == 12).Valor);
        }
    }
}