using Application;
using Infrastructure;
using System.Threading.RateLimiting;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Protect API, with specific IP only 10 reqs/sencond
            builder.Services.AddRateLimiter(option =>
            {
                option.AddPolicy("PerIpPolicy", context =>
                {
                    var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknow_ip";

                    return RateLimitPartition.GetSlidingWindowLimiter(clientIp, partition => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromSeconds(1),
                        SegmentsPerWindow = 1,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    });
                });
            });

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
        }
    }
}
