using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestGrpcCaller.Models;
using TestWebApiApplication;
using TestWebApiApplication.Shared;

namespace TestGrpcCaller.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private readonly Greeter.GreeterClient client;

		private readonly ILogger<TestController> logger;

		public TestController(Greeter.GreeterClient client, ILogger<TestController> logger)
		{
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		public async Task<GrpcCallerResponse> Get(CancellationToken cancellationToken)
		{
			logger.LogInformation("Executing test gRPC call ...");

			var request = new HelloRequest
			{
				Name = "Test",
			};

			var response = await client.SayHelloAsync(request, cancellationToken: cancellationToken);

			logger.LogInformation("Executed test gRPC call");

			return new GrpcCallerResponse
			{
				Version = $"Test gRPC Caller ({VersionHelper.GetVersion<TestController>()})",
				Response = response.Message,
			};
		}
	}
}
