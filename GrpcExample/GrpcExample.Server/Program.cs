var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();

var app = builder.Build();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});
app.MapDefaultEndpoints();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
