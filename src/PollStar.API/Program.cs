using HexMaster.RedisCache;
using PollStar.Core.Configuration;
using PollStar.Polls;
using PollStar.Sessions;
using PollStar.Users;

const string defaultCorsPolicyName = "default_cors";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AzureStorageConfiguration>(
    builder.Configuration.GetSection(AzureStorageConfiguration.SectionName));

builder.Services.AddHexMasterCache(builder.Configuration);
builder.Services.AddPollStarUsers();
builder.Services.AddPollStarSessions();
builder.Services.AddPollStarPolls();

builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: defaultCorsPolicyName,
        bldr =>
        {
            bldr.WithOrigins("http://localhost:4200",
                    "https://gray-mushroom-0bd85e303.1.azurestaticapps.net",
                    "https://roulette.hexmaster.nl")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(defaultCorsPolicyName);

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.UseEndpoints(ep =>
{
    ep.MapHealthChecks("/health");
    ep.MapControllers();
});

app.Run();
