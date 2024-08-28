using MessageServiceApi.MessageServiceApi.DAL;
using MessageServiceApi.MessageServiceApi.DAL.Interface;
using MessageServiceApi.MessageServiceApi.Service.Implementations;
using MessageServiceApi.MessageServiceApi.Service.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


