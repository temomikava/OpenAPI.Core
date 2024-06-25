using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using OpenAPI.Core;
using OpenAPI.Core.CommandHandlers;
using OpenAPI.Core.Data;
using OpenAPI.Core.IntegrationEventHandlers;
using OpenAPI.Core.Services;
using SharedKernel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();


builder.Services.AddScoped<IIntegrationEventService, IntegrationEventService>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddSingleton<ServiceA>();
builder.Services.AddSingleton<ServiceB>();
builder.Services.AddSingleton<IProcessingServiceFactory, ProcessingServiceFactory>();
builder.Services.AddMediatR(typeof(ProcessPaymentCommandHandler).Assembly);


builder.Services.AddMassTransit(configuration =>
{
    configuration.AddConsumers(typeof(OrderPorcessingIntegrationEventHandler).Assembly);
    configuration.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("tmikava");
            h.Password("Npottwyctd12");
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
}

app.Run();
