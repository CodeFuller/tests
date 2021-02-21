using System;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TestWebApiApplication.Services
{
	public class GreeterService : Greeter.GreeterBase
	{
		private readonly ILogger<GreeterService> logger;

		public GreeterService(ILogger<GreeterService> logger)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
		{
			logger.LogInformation("Processing test gRPC request ...");

			return Task.FromResult(new HelloReply
			{
				Message = "Hello " + request.Name
			});
		}
	}
}
