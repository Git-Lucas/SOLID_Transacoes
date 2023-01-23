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
        var transacaoUseCase = new TransacaoUseCase(transacaoData);
        var id = new Random().Next(1000, 9999).ToString();
        var result = await transacaoUseCase.CriarAsync(id, transacao.Valor, transacao.NumeroParcelas, transacao.MetodoPagamento);
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
    var transacaoUseCase = new TransacaoUseCase(transacaoData);
    return Results.Ok(await transacaoUseCase.VisualizarPorIdAsync(id));
});

app.MapGet("/transacoes", async (EfSqliteAdapter context) =>
    {
        var transacaoData = new TransacaoDataSqlite(context);
        var transacaoUseCase = new TransacaoUseCase(transacaoData);
        Results.Ok(await transacaoUseCase.VisualizarTodasAsync());
    });

app.MapDelete("transacoes/{id}", async (EfSqliteAdapter context, string id) =>
{
    var transacaoData = new TransacaoDataSqlite(context);
    var transacaoUseCase = new TransacaoUseCase(transacaoData);
    await transacaoUseCase.DeletarAsync(id);

    return Results.Ok();
});

app.MapDelete("transacoes/deletarTodos", async (EfSqliteAdapter context) =>
{
    var transacaoData = new TransacaoDataSqlite(context);
    var transacaoUseCase = new TransacaoUseCase(transacaoData);
    var transacoes = await transacaoUseCase.VisualizarTodasAsync();

    foreach (Transacao t in transacoes)
    {
        await transacaoUseCase.DeletarAsync(t.Id);
    }
});

app.Run();