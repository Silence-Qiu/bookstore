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
        o.DefaultLimit = 10; // 设置默认的分页条目数
        o.MaxLimit = 50; // 设置最大的分页条目数
    });
})
.AddNewtonsoftJson(setup =>
 {
     //加入json校验的轮子
     //轮子可以从构造函数知道那些是require
     //轮子可以检查哪些不能为null
     setup.SerializerSettings.ContractResolver =
     new StrictContractResolver(
         //驼峰命名
         new CamelCasePropertyNamesContractResolver()
         );
     //忽略循环引用
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
