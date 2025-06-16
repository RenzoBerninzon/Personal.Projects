using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.BusinessMS.Users.Application.Query;
using Store.BusinessMS.Users.Application;
using Store.BusinessMS.Users.Infrastructure;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<Azure.Core.Serialization.NewtonsoftJsonObjectSerializer>();

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlConnectionString") ?? Environment.GetEnvironmentVariable("SqlConnectionString");

builder.Services.AddPersistenceInfrastructure(sqlConnectionString);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserById).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Build().Run();
