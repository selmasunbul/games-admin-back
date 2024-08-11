using business.Abstract;
using business.Base;
using business.Concrete;
using business.IdentityErrorLocalization;
using Business.Abstract;
using Business.Base;
using Business.Concrete;
using data_access.Abstract;
using data_access.context;
using data_access.entities;
using games_admin_back.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

string connectionString = builder.Configuration.GetConnectionString("MysqlDbContextConnection")
    ?? throw new InvalidOperationException("MysqlDbContextConnection is missing or invalid.");

builder.Services.AddDbContext<MysqlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<MongoDbContext, MongoDbContext>();
builder.Services.AddTransient(typeof(IMongoServiceBase<>), typeof(MongoServiceBase<>));
builder.Services.AddTransient<IConfigurationService, ConfigurationService>();





builder.Services.AddIdentity<AppUser, AppRole>(o =>
{
    o.SignIn.RequireConfirmedAccount = false;
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = true;
    o.Password.RequireUppercase = true;
    o.Password.RequireNonAlphanumeric = true;
    o.Password.RequiredLength = 8;
    o.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
})
.AddRoles<AppRole>()
.AddErrorDescriber<LocalizedIdentityErrorDescriber>()
.AddEntityFrameworkStores<MysqlDbContext>()
.AddDefaultTokenProviders();


#region JWT Authentication
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
var jwtKey = builder.Configuration["JwtKey"];
byte[] key;
if (jwtKey != null)
{
    key = Encoding.ASCII.GetBytes(jwtKey);
}
else
{
    // JwtKey deðeri null ise hata ver
    throw new InvalidOperationException("JwtKey is missing or invalid.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });
#endregion

builder.Services.AddHttpContextAccessor();


#region SWAGGER
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Games Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.OperationFilter<AuthOperation>();

});
#endregion
builder.Services.AddCors(options =>
{
    options.AddPolicy("GeneralCors", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();


//#region Apply Migrations Automatically
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<MysqlDbContext>();
//    context.Database.Migrate();
//}
//#endregion




app.UseCors("GeneralCors");


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelExpandDepth(2);
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelRendering(ModelRendering.Example);
    c.EnableTryItOutByDefault();
    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CO CARRY");
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
