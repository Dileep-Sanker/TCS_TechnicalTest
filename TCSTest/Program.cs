using Microsoft.Win32;
using TCSTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register ContentService as a singleton
builder.Services.AddSingleton<IContentService, ContentService>();
// Register ChannelService as a singleton
builder.Services.AddSingleton<IChannelService, ChannelService>();
// Register ScheduleService as a singleton
builder.Services.AddSingleton<IChannelScheduleService, ChannelScheduleService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
