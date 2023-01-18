using Microsoft.AspNetCore.Mvc;
using SOLID_Transacoes.Application;
using SOLID_Transacoes.Domain.Models;
using SOLID_Transacoes.Infra.Data;
using SOLID_Transacoes.Infra.EntityFramework;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EfSqliteAdapter>();

var app = builder.Build();

app.MapPost("/transacoes", async (EfSqliteAdapter context, [FromBody] Transacao transacao) =>
{
    try
    {
        var transacaoData = new TransacaoDataSqlite(context);
        var criaTransacaoUseCase = new CriaTransacao(transacaoData);
        var id = new Random().Next(1000, 9999).ToString();
        var result = await criaTransacaoUseCase.ExecutaAsync(id, transacao.Valor, transacao.NumeroParcelas, transacao.MetodoPagamento);
        return Results.Created($"/transacoes/{id}", result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/transacoes/{id}", async (EfSqliteAdapter context, string id) =>
{
    var transacaoData = new TransacaoDataSqlite(context);
    var visualizaTransacao = new VisualizaTransacao(transacaoData);
    return Results.Ok(await visualizaTransacao.ExecutaAsync(id));
});

app.MapGet("/transacoes", async (EfSqliteAdapter context) =>
    Results.Ok(await new TransacaoDataSqlite(context).GetAllAsync()));

app.MapDelete("transacoes/{id}", async (EfSqliteAdapter context, string id) =>
{
    var deletaTransacao = new DeletaTransacao(new TransacaoDataSqlite(context));
    await deletaTransacao.ExecutaAsync(id);

    return Results.Ok();
});

//app.MapDelete("transacoes/deletarTodos", async (EfSqliteAdapter context) =>
//{
//    var transacaoData = new TransacaoDataSqlite(context);
//    var visualizaTransacoes = new VisualizaTransacao(transacaoData);
//    var deletaTransacao = new DeletaTransacao(transacaoData);
//    var transacoes = await visualizaTransacoes.ExecutaAsync();

//    foreach (Transacao t in transacoes)
//    {
//        await deletaTransacao.ExecutaAsync(t.Id);
//    }
//});

app.Run();