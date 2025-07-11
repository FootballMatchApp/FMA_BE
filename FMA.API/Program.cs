using FMA.API.Extensions.BuilderExtensions;
using FMA.API.Extensions.PolicyExtensions;
using FMA.API.Extensions.ServiceRegistration;
using FMA.API.Extensions.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


builder.Services.RegisterAllServices(builder.Configuration);

//Add config
builder.AddAppConfiguration();

//Add CORS policy
builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



// Add custom middlewares
app.UseApplicationMiddlewares();

// Add CORS
app.UseCorsPolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Add Routing
//app.MapCustomEndpoints();



app.Run();
