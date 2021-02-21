using AspNetMonsters.ApplicationInsights.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestWebApiApplication.Services;
using TestWebApiApplication.Shared;

namespace TestWebApiApplication
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddGrpc();

			services.AddApplicationInsightsTelemetry();
			services.AddApplicationInsightsKubernetesEnricher();
			services.AddCloudRoleNameInitializer($"Test Web API Application ({VersionHelper.GetVersion<Startup>()})");
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				endpoints.MapGrpcService<GreeterService>();
			});
		}
	}
}
