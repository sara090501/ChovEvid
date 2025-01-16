namespace ChovEvid
{
    using ChovEvid.Repositories;
    using Microsoft.Extensions.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPersonRepository, PersonRepository>(provider =>
                new PersonRepository(_connectionString));

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
        }
    }
}