using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var tnsAdminPath = builder.Configuration.GetValue<string>("Oracle:TnsAdmin");
        var walletLocation = builder.Configuration.GetValue<string>("Oracle:TnsAdmin");

        Environment.SetEnvironmentVariable("TNS_ADMIN", tnsAdminPath);
        OracleConfiguration.TnsAdmin = tnsAdminPath;
        OracleConfiguration.WalletLocation = walletLocation;

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseOracle(builder.Configuration.GetConnectionString("OracleDB"));
        });

        builder.Services.AddScoped(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var conn = new OracleConnection(config.GetConnectionString("OracleDB"));
            return conn;
        });

        //  JWT konfiguracja
        var key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");



        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TwojeAPI", Version = "v1" });

            // 🔐 Konfiguracja JWT w Swaggerze
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Wpisz: Bearer [spacja] i token JWT"
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication(); // musi być przed UseAuthorization
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
