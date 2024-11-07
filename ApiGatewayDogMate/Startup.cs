using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGatewayDogMate;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOcelot().AddSingletonDefinedAggregator<FakeDefinedAggregator>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseOcelot().Wait();
    }
    
}
