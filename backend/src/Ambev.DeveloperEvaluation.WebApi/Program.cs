using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rebus.Handlers;
using Serilog;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Application.Carts.Handlers;
using Ambev.DeveloperEvaluation.Application.Products.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Configuração do Serilog
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            // Configuração do DbContext
            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            // Adiciona serviços
            builder.Services.AddControllers()
             .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });
            builder.Services.AddCors(options =>
      {
          options.AddPolicy("AllowSpecificOrigin",
              builder => builder.WithOrigins("http://localhost:4200") // Adicione o domínio do seu frontend
                                .AllowAnyMethod()
                                .AllowAnyHeader());
      });

            builder.Services.AddControllers()
              .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.PropertyNamingPolicy = null; // Para manter a nomenclatura original
                  options.JsonSerializerOptions.IgnoreNullValues = true; // Ignora valores nulos
              });

            builder.Services.AddControllers(); // ou services.AddMvc();

            builder.Services.AddEndpointsApiExplorer();
            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            // Configuração do Rebus
            builder.Services.AddRebus(configure => configure
             .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost", "my_queue"))
             .Routing(r => r.TypeBased().Map<PlaceOrderCommand>("my_queue")) // Mapeia PlaceOrderCommand para a fila "my_queue"
            );

            // Adiciona o manipulador de mensagens
            builder.Services.AddTransient<IHandleMessages<PlaceOrderCommand>, PlaceOrderCommandHandler>();
            builder.Services.AddTransient<IOrderService, OrderService>();

            // Configuração de autenticação JWT
            builder.Services.AddJwtAuthentication(builder.Configuration);

            // Registro de dependências
            builder.RegisterDependencies();
            // Registro do AutoMapper
            builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
            // Registro do AutoMapper
            builder.Services.AddAutoMapper(typeof(AuthProfile).Assembly); // Adicione seu perfil aqui
            // Configuração do AutoMapper
            builder.Services.AddAutoMapper(typeof(AuthenticateUserProfile).Assembly); // Registra o perfil de mapeamento
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(UpdateCartCommandHandler).Assembly);
            builder.Services.AddAutoMapper(typeof(DeleteCartCommandHandler).Assembly);
            builder.Services.AddAutoMapper(typeof(UpdateProductCommandHandler).Assembly);
            builder.Services.AddAutoMapper(typeof(DeleteProductCommandHandler).Assembly);
            // Configuração do AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddMediatR(typeof(GetCartsQueryHandler).Assembly); // Registra o MediatR e todos os manipuladores na mesma assembly
            builder.Services.AddMediatR(typeof(GetProductByIdHandler).Assembly); // Registra o MediatR e todos os manipuladores na mesma assembly
            builder.Services.AddMediatR(typeof(GetCartByIdQueryHandler).Assembly);
            // Configuração do MediatR
            builder.Services.AddMediatR(typeof(ApplicationLayer).Assembly, typeof(Program).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            // Use a política de CORS
            app.UseCors("AllowSpecificOrigin");
            app.UseExceptionHandler("/error"); // Redireciona para um endpoint de erro
            app.UseHsts();

            app.UseMiddleware<ValidationExceptionMiddleware>();

            // Configura o pipeline de middleware.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Erro genérico
                app.UseHsts();
            }
            app.MapControllers(); // Mapeia os controladores
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseBasicHealthChecks();
            app.MapControllers();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

}