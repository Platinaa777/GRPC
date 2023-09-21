using Microsoft.EntityFrameworkCore;
using MyGRPC.Data;
using MyGRPC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<DataContext>(config =>
{
    config.UseSqlite("DataSource=mydata.db");
});
var app = builder.Build();

app.MapGrpcService<UsersService>();
app.Map("/", () => Console.Write("123"));

app.Run();