using System.Reflection;
using eCommerce.Host;
using eCommerce.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = LocalConfigurationExtentions.GetConfigurationBuilder();

builder.Services.AddSingleton(configuration);
builder.Services.AddControllers();
builder.Services.AddApplication<eCommerceHostModule>();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();