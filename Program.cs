using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MultipleDBContext.Api.Contracts;
using MultipleDBContext.Api.DataBase;
using System.Text.Json.Serialization;
using MultipleDBContext.Api.Orders;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;

namespace MultipleDBContext.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var conn = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ProductsDbContext>(d => d.UseSqlServer(conn, o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "MyDemoProducts")));
            builder.Services.AddDbContext<OrderDbContext>(d => d.UseSqlServer(conn,o=>o.MigrationsHistoryTable(HistoryRepository.DefaultTableName,"MyDemoOrders")));//.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            var app = builder.Build();

            app.MapGet("products", async (ProductsDbContext productsContext) =>
            {
                return Results.Ok(productsContext.Products.Select(d => d.Id).ToArrayAsync());
            })
            .WithName("GetProducts")
            .WithDescription("Get all product IDs")
            .WithSummary("Fetch Products");

            app.MapPost("orders", async (SubmitOrderRequest request, 
                                         ProductsDbContext productContext,
                                         OrderDbContext ordersContext) =>
            {
                var products = await productContext.Products.Where(p => request.ProductIds.Contains(p.Id)).AsNoTracking().ToListAsync();

                if(products.Count!= request.ProductIds.Count)
                {
                    return Results.BadRequest("Some Product is missing");
                }

               // using var transaction = new SqlTransaction();

                //productContext.Database.UseTransaction(transaction);
                //ordersContext.Database.UseTransaction(transaction);

                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    TotalPrice = products.Sum(d => d.Price),
                    LineItems = products.Select(p => new LineItem
                    {
                        Price = p.Price,
                        Id = Guid.NewGuid(),
                        ProductId = p.Id
                    }).ToList()
                };

                ordersContext.Orders.Add(order);

                await ordersContext.SaveChangesAsync();

                return Results.Ok(order);
            })
            .WithName("SubmitOrder")
            .WithDescription("Submit a new order with product IDs")
            .WithSummary("Create Order");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            Todo[] sampleTodos =
            [
                new(1, "Walk the dog"),
                new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
                new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                new(4, "Clean the bathroom"),
                new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
            ];

            var todosApi = app.MapGroup("/todos")
                .WithDescription("Operations related to todos")
                .WithSummary("Todo Operations");

            todosApi.MapGet("/", () => sampleTodos)
                    .WithName("GetTodos")
                    .WithDescription("Get all todos")
                    .WithSummary("Fetch All Todos");

            todosApi.MapGet("/{id}", Results<Ok<Todo>, NotFound> (int id) =>
                sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
                    ? TypedResults.Ok(todo)
                    : TypedResults.NotFound())
                .WithName("GetTodoById")
                .WithDescription("Get a specific todo by ID")
                .WithSummary("Fetch Todo by ID");

            app.Run();
        }
    }

    public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

    [JsonSerializable(typeof(Todo[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
