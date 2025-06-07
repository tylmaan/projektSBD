using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

var builder = WebApplication.CreateBuilder(args);

var tnsAdminPath = builder.Configuration.GetValue<string>("Oracle:TnsAdmin");
var walletLocation = builder.Configuration.GetValue<string>("Oracle:TnsAdmin");

Environment.SetEnvironmentVariable("TNS_ADMIN", tnsAdminPath);

Oracle.ManagedDataAccess.Client.OracleConfiguration.TnsAdmin = tnsAdminPath;
Oracle.ManagedDataAccess.Client.OracleConfiguration.WalletLocation = walletLocation;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDB"));
});

builder.Services.AddScoped<OracleConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var conn = new OracleConnection(config.GetConnectionString("OracleDB"));
    return conn;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfiguracja request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
