
using EvosancomAPI.Persistence;
using EvosancomAPI.Application;
using EvosancomAPI.Infrastructure.Services.Storage.Local;
using EvosancomAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using Serilog.Context;
using EvosancomAPI.API.Configurations.ColumnWriters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Logger log = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("logs/log.txt")
	.WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("DefaultConnection")  ,
	"logs",needAutoCreateTable:true,
	columnOptions: new Dictionary<string, ColumnWriterBase>
	{
		{"message", new RenderedMessageColumnWriter() }
		,
		{"message_template", new MessageTemplateColumnWriter() },
		{"level",new LevelColumnWriter() },
		{"time_stamp", new TimestampColumnWriter() },
		{"exception", new ExceptionColumnWriter() },
		{"log_event", new LogEventSerializedColumnWriter() },
		{"user_name" , new UsernameColumnWriter() }
		
	}
	)
	.Enrich.FromLogContext() // harici propertyler için //contextten beslen
	.MinimumLevel.Information()
	.CreateLogger();
builder.Host.UseSerilog(log);


builder.Services.AddHttpLogging(logging =>
{
	logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
	logging.RequestHeaders .Add("sec-ch-ua");//kullancıya dair tüm bilgileri getirir
	logging.MediaTypeOptions.AddText("application/javascript");
	logging.RequestBodyLogLimit = 4096;
	logging.ResponseBodyLogLimit = 4096;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//builder.Services.AddStorage<LocalStorage>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new()
	{
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidAudience = builder.Configuration["Token:Audience"],
		ValidIssuer = builder.Configuration["Token:Issuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
		LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
			expires != null ? expires > DateTime.UtcNow : false,
		NameClaimType = ClaimTypes.Name
	};

	options.Events = new JwtBearerEvents
	{
		OnAuthenticationFailed = context =>
		{
			Console.WriteLine("Token doğrulama hatası: " + context.Exception.Message);
			return Task.CompletedTask;
		},
		OnTokenValidated = context =>
		{
			Console.WriteLine("Token başarıyla doğrulandı");
			return Task.CompletedTask;
		}
	};
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseSerilogRequestLogging(); // her istekte loglama yapar
								// loglanmasını istediğimiz şeylerin üstüne koyarız
app.UseHttpLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


// araya giriyoruz
// bir middleware oluşturucaz
// authentication olmuşsa


app.Use(async (context, next) =>
{
	var isAuth = context.User?.Identity?.IsAuthenticated == true;
	var username = isAuth ? context.User.Identity.Name : "anonymous";

	Console.WriteLine($"Is Authenticated: {isAuth}, Username: {username}");

	LogContext.PushProperty("user_name", username);
	await next();
});


app.MapControllers();


app.Run();
