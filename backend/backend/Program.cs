// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                builder => builder.WithOrigins("http://localhost:3000")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });

        // Add services to the container.

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
        app.UseCors("AllowLocalhost");

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
