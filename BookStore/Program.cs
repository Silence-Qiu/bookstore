using BookStore.Data;
using BookStore.DTO;
using Microsoft.EntityFrameworkCore;
using MQuery.AspNetCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stargazer.Extensions.Newtonsoft.Json.StrictContract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerDocument();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BookStoreDbContext>(op => op.UseSqlite("Data Source=bookstore.db"));

builder.Services.AddControllers(options =>
{
    options.AddMQuery(o =>
    {
        o.DefaultLimit = 10; 
        o.MaxLimit = 50;
    });
})
.AddNewtonsoftJson(setup =>
 {
     setup.SerializerSettings.ContractResolver =
     new StrictContractResolver(
         new CamelCasePropertyNamesContractResolver()
         );
     setup.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
     setup.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
 }); ;


builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();

    app.UseSwaggerUi3();
}

var options = new DefaultFilesOptions();
options.DefaultFileNames.Add("index.html");

app.UseDefaultFiles(options);

app.UseStaticFiles();

app.MapControllers();

app.Run();
