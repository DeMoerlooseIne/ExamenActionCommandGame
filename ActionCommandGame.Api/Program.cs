using ActionCommandGame.Api.Installers.Extensions;
using ActionCommandGame.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


var corsPolicyName = "CorsName";

// Install services to the container using IInstaller classes.
builder.Services.InstallServicesInAssembly(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.DefaultPolicyName = corsPolicyName;
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

//Initialize dbContext data
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ActionCommandGameDbContext>();

//Use this one for the database connection + uncomment 
//builder.Services.AddDbContext<ActionCommandGameDbContext>
//    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("ActionCommandGame")));

//Run once to migrate DBContent from InMemory to SQL
//dbContext.Initialize();

if (dbContext.Database.IsInMemory())
{
    dbContext.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Action Command Game API v1"));
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
