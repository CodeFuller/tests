using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AspNetMonsters.ApplicationInsights.AspNetCore;
using TestWebApiApplication;
using TestWebApiApplication.Shared;

namespace TestGrpcCaller
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddGrpcClient<Greeter.GreeterClient>(o =>
			{
				var settings = new AppSettings();
				configuration.Bind(settings);

				o.Address = settings.TestAppUri;
			});

			services.AddApplicationInsightsTelemetry();
			services.AddApplicationInsightsKubernetesEnricher();
			services.AddCloudRoleNameInitializer($"Test gRPC Caller ({VersionHelper.GetVersion<Startup>()})");
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
			});
		}
	}
}
