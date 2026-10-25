using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Distributed;
using MultipleDBContext.Api.Caching;
using MultipleDBContext.Api.Contracts;
using MultipleDBContext.Api.DataBase;
using MultipleDBContext.Api.Orders;
using MultipleDBContext.Api.Shipments;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using System.Transactions;

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
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddStackExchangeRedisCache(d =>
            {
                d.Configuration = builder.Configuration.GetConnectionString("Redis");

            });
            builder.Services.AddSingleton<ICacheService, CacheService>();

            var conn = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ProductsDbContext>(d => d.UseSqlServer(conn, o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "MyDemoProducts")));
            builder.Services.AddDbContext<OrderDbContext>(d => d.UseSqlServer(conn, o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "MyDemoOrders")));
            builder.Services.AddDbContext<CustomerDBContext>(options => options.UseSqlServer(conn, sql => sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "MyDemoCustomers")));
            builder.Services.AddDbContext<ShipmentDbContext>(options => options.UseSqlServer(conn, sql => sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "MyDemoShipments")));

            var app = builder.Build();


            static async Task<long[]> GetProductIDs(ProductsDbContext productsDbContext)
            {
                var productIds = await productsDbContext.Products.Select(d => d.Id).ToArrayAsync();
                return productIds;
            }


            app.MapGet("Shipments", async (ShipmentDbContext shipmentDb, CancellationToken token) =>
            {
                var shipment = await shipmentDb.Shipments.AsNoTracking().Where(d => d.Shipping.City == "Samsun")
                .Select(o => new { o.Id,o.ShipmentNumber, o.Shipping }).ToListAsync();
                return Results.Ok(shipment);

            });

            app.MapPut("Shipments/{Id}", async (int Id, ShipmentDbContext shipmentDb, CancellationToken token) =>
            {
                var shipment = await shipmentDb.Shipments.SingleAsync(c => c.Id == Id);
                shipment.Shipping = shipment.Shipping with { TrackingNumber = "D0-555" };
                await shipmentDb.SaveChangesAsync();
                return Results.Ok();
            });

            app.MapGet("AddShipment", async (ShipmentDbContext shipmentDb, CancellationToken token) =>
            {
                Shipment entity = new Shipment()
                {
                    ShipmentNumber = "001",
                    Shipping = new ShippingDetails()
                    {
                        City = "Samsun",
                        Country = "Türkiye",
                        Recipient = "Volkan",
                        TrackingNumber = "55-001"
                    },
                    CreatedAt = DateTime.Now,
                    Notes = new List<ShipmentNote>
                       {
                           new ShipmentNote(
                             "Bunu yazan tosun okuayna kosun",
                             DateTime.Now),

                              new ShipmentNote(
                             "Hasan yazan tosun",
                             DateTime.Now)

                       }

                };

                shipmentDb.Shipments.Add(entity);

                await shipmentDb.SaveChangesAsync();
            });


            app.MapGet("rediscache2", async (ProductsDbContext productsDbContest,
                                               IDistributedCache _distributedCache,
                                               CancellationToken token = default) =>
            {
                var key = "rediscache2";
                var result = await GetProductIDs(productsDbContest);
                string cachedValue = JsonConvert.SerializeObject(result, Formatting.Indented);
                await _distributedCache.SetStringAsync(key, cachedValue,
                                                        new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) },
                                                        token);

                return Results.Ok(result);
            });


             app.MapGet("products-cache2", async (ProductsDbContext productsDbContest,
                                                ICacheService _cacheService,
                                                CancellationToken token = default) =>
            {
                var key = "prod_cache2";
                long[]? prodResponse = await _cacheService.GetAsync<long[]>(key, async () =>
                {
                    var result = await GetProductIDs(productsDbContest);
                    await _cacheService.SetAsync<long[]>(key, result, token);
                    return result;

                }, token);

                return Results.Ok(prodResponse);
            });

            app.MapGet("products-cache", async (ProductsDbContext productsDbContest, 
                                                ICacheService _cacheService,
                                                CancellationToken token = default) =>
            {
                var key = "prod_cache";
                long[]? prodResponse = await _cacheService.GetAsync<long[]>(key, token);
                if (prodResponse != null)
                {
                    return Results.Ok(prodResponse);
                }

                long[] dbRec = await GetProductIDs(productsDbContest);

                await _cacheService.SetAsync<long[]>(key, dbRec, token);

                return Results.Ok(dbRec);
            });

            app.MapGet("products", async (ProductsDbContext productsContext,IDistributedCache distributedCache) =>
            {
                string key = "products";
                // var productIds = await productsContext.Products.Select(d => d.Id).ToArrayAsync();

                var cacheProuct = await distributedCache.GetOrCreateAsync<long[]>(key, async fc =>
                {
                    return await GetProductIDs(productsContext);
                });

                if(cacheProuct is null) { return Results.NotFound(); }  


                return Results.Ok(cacheProuct);
            })
            .WithName("GetProducts")
            .WithDescription("Get all product IDs")
            .WithSummary("Fetch Products");

#if DEBUG
            app.MapGet("cache-lock-test", async (IDistributedCache distributedCache, CancellationToken cancellationToken) =>
            {
                var key = $"cache-lock-test:{Guid.NewGuid():N}";

                async Task<Guid[]> TestFactory(CancellationToken token)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), token);
                    return [Guid.NewGuid()];
                }

                var firstRequest = distributedCache.GetOrCreateAsync<Guid[]>(key, TestFactory, cancellationToken: cancellationToken);
                var secondRequest = distributedCache.GetOrCreateAsync<Guid[]>(key, TestFactory, cancellationToken: cancellationToken);
                var results = await Task.WhenAll(firstRequest, secondRequest);

                return Results.Ok(new { key, results });
            });
#endif

            app.MapPost("orders", async (SubmitOrderRequest request, 
                                         ProductsDbContext productContext,
                                         OrderDbContext ordersContext) =>
            {
                // TransactionScope - Her iki DbContext'i aynı transaction'da tut
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        // 1. ProductsDbContext - Tracked (güncellemek için)
                        var products = await productContext.Products
                            .Where(p => request.ProductIds.Contains(p.Id))
                            .ToListAsync(); // Tracked şekilde oku

                        if(products.Count != request.ProductIds.Count)
                        {
                            return Results.BadRequest("Some Product is missing");
                        }

                        // Güncelle (örn: Stock azalt veya başka bir property güncelle)
                        products.ForEach(p => p.Price -= 10);
                        // Eğer Product'da Stock property'si varsa uncomment et
                        await productContext.SaveChangesAsync();

                        // 2. OrderDbContext - Yeni order oluştur
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

                        transactionScope.Complete(); // ✅ İkisi de commit
                        return Results.Ok(order);
                    }
                    catch (Exception ex)
                    {
                       
                        // ❌ Hata varsa ikisi de rollback
                        return Results.BadRequest($"Order creation failed: {ex.Message}");
                    }
                }
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
