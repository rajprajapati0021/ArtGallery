using ArtGallery.Configuration;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.ConfigureMySql(configuration);
    builder.Services.ConfigureAuthentication(configuration);
    builder.Services.AddDependencyConfiguration(configuration);
    builder.Logging.AddConsole();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins",
            policy =>
            {
                policy.WithOrigins("https://artgallery-production-d03d.up.railway.app", "http://localhost:4200")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowSpecificOrigins");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Unhandled exception: {ex}");
    throw; // Re-throw to preserve the behavior
}


//await app.RunAsync();

