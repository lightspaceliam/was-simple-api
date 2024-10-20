using api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
	.AddNewtonsoftJson(options => //  Register NewtonSoft and include configuration for HL7 FHIR
	{
		options.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset;
		options.SerializerSettings.Converters.Add(new FhirResourceConverter());
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy
	.AllowAnyMethod()
	.AllowAnyHeader()
	.AllowAnyOrigin());
app.MapControllers();

app.Run();
