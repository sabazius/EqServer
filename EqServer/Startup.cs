using EqModels.Models;
using EqServer.BL.Generator;
using EqServer.BL.Interfaces;
using EqServer.BL.Services;
using EqServer.DataLayer.Kafka;
using EqServer.DL.Interfaces;
using EqServer.DL.Kafka;
using EqServer.DL.Kafka.Producers;
using EqServer.DL.Mongo;
using EqServer.DL.Repositories;
using EqServer.EqModels.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EqServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen();

            services.Configure<MongoDbConfiguration>(Configuration.GetSection(nameof(MongoDbConfiguration)));

            var mongoSettings = Configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>();

            services.AddSingleton<IPackGenerator, PackGenerator>();
            services.AddSingleton<KafkaAdmin>();
            services.AddSingleton<ICalculationPackProducer, CalculationPackProducer>();
            services.AddSingleton<IResultsRepository, ResultsRepository>();

            services.AddSingleton<ICalculationPackRepository, CalculationPackRepository>();
            services.AddSingleton<ICalculationPackService, CalculationPackService>();

            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper(typeof(Startup));

            services.AddHostedService<ResultConsumer>();
        }

        private Equation GetEquation()
        {
            return new Equation()
            {
                Id = 1,
                EqMethod = "a*x+b",
                Result = 0
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EqServer API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
