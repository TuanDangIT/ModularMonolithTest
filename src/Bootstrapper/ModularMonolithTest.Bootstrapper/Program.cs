using Microsoft.AspNetCore.Http.HttpResults;
using ModularMonolithTest.Shared.Infrastructure;
using System.Reflection;
using ModularMonolithTest.Modules.FirstData.Api;
using ModularMonolithTest.Modules.SecondData.Api;
using ModularMonolithTest.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddFirstData(builder.Configuration);
//builder.Services.AddSecondData(builder.Configuration);
var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
var modules = ModuleLoader.LoadModules(assemblies);
foreach (var module in modules)
{
    module.Register(builder.Services, builder.Configuration);
}
builder.Services.AddInfrastructure(assemblies, modules);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.UseInfrastructure();
foreach (var module in modules)
{
    module.Use(app);
}
app.Run();
