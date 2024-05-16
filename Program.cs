using infraAlerta.Data;
using infraAlerta.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using dotenv.net;


var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { dotenv }));

var builder = WebApplication.CreateBuilder(args);

var connectionStringMysql = builder.Configuration["CONNECTION_MYSQL"];

// Add services to the container.
builder.Services.AddDbContext<ApiDbContext>(options => options.UseMySql(
    connectionStringMysql,
    ServerVersion.AutoDetect(connectionStringMysql)
));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<infraAlerta.Helper.ISession, Session>();
builder.Services.AddScoped<IEmail, Email>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
               builder =>
               {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
