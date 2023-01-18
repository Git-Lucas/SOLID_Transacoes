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
                Id = new Random().Next(1000, 9999).ToString(),
                Valor = 1000,
                NumeroParcelas = 12,
                MetodoPagamento = MetodoPagamento.CartaoCredito
            };

            transacao.GeraParcelas();

            //Count sem o parênteses, porque não é necessária a expressão LINQ
            Assert.AreEqual(12, transacao.Parcelas.Count);
            Assert.AreEqual(83.33, transacao.Parcelas.First(x => x.NumeroParcela == 1).Valor);
            Assert.AreEqual(83.37, transacao.Parcelas.First(x => x.NumeroParcela == 12).Valor);
        }
    }
}