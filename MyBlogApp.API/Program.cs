using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBlogApp.Application.Commands.CreatePost;
using MyBlogApp.Application.Commands.DeletePost;
using MyBlogApp.Application.Commands.UpdatePost;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Application.Queries.GetAllPosts;
using MyBlogApp.Application.Queries.GetPostById;
using MyBlogApp.Application.Validators;
using MyBlogApp.Infrastructure.Data;
using MyBlogApp.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreatePostCommandValidator>();

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

// Configure Serilog for logging
builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration));

// Add DbContext with In-Memory provider
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseInMemoryDatabase("MyBlogAppDb"));

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(CreatePostCommandHandler).Assembly,
        typeof(UpdatePostCommandHandler).Assembly,
        typeof(DeletePostCommandHandler).Assembly,
        typeof(GetPostByIdQueryHandler).Assembly,
        typeof(GetAllPostsQueryHandler).Assembly
    );
});

// Register repository
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlogApp API", Version = "v1" });
});

// Add Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBlogApp API v1"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Map Health Checks
app.MapHealthChecks("/health");

Log.Information("Application started and running");

app.Run();