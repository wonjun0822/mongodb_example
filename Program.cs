using mongodb_example.Config;
using mongodb_example.Interface;
using mongodb_example.Service;
using mongodb_example.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<MongoDBoptionsSetup>();

builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddSingleton<IArticleService, ArticleService>();
builder.Services.AddSingleton<ICommentService, CommentService>();
builder.Services.AddSingleton<IReplyService, ReplyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
