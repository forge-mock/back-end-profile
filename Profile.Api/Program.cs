using DotNetEnv;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
    options.AddPolicy(
        "Development",
        policy =>
        {
            policy.WithOrigins("https://localhost:4001")
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithHeaders("Content-Type");
        }));
// builder.Services.AddDbContext<AuthContext>(options =>
//     options.UseNpgsql(Environment.GetEnvironmentVariable("USER_IDENTITY_DB_CONNECTION_STRING")));
builder.Services.AddControllers();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => options
        .WithTheme(ScalarTheme.Purple)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http));
}

app.UseCors("Development");
app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();