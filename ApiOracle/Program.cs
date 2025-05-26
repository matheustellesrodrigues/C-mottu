using ApiOracle.Data;
using ApiOracle.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativa Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Rotas da API

// GET: Todos os produtos
app.MapGet("/produtos", async (AppDbContext db) =>
    await db.Produtos.ToListAsync());

// GET: Produto por ID (PathParam)
app.MapGet("/produtos/{id}", async (int id, AppDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);
    return produto is not null ? Results.Ok(produto) : Results.NotFound();
});

// GET: Produto por nome (QueryParam)
app.MapGet("/produtos/search", async (string nome, AppDbContext db) =>
{
    var produtos = await db.Produtos
        .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
        .ToListAsync();

    return produtos.Any() ? Results.Ok(produtos) : Results.NotFound();
});

// POST
app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
{
    db.Produtos.Add(produto);
    await db.SaveChangesAsync();
    return Results.Created($"/produtos/{produto.Id}", produto);
});

// PUT
app.MapPut("/produtos/{id}", async (int id, Produto produtoAtualizado, AppDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null) return Results.NotFound();

    produto.Nome = produtoAtualizado.Nome;
    produto.Preco = produtoAtualizado.Preco;

    await db.SaveChangesAsync();
    return Results.Ok(produto);
});

// DELETE
app.MapDelete("/produtos/{id}", async (int id, AppDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null) return Results.NotFound();

    db.Produtos.Remove(produto);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

#endregion

app.Run();


