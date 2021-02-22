using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestHttpCaller.Models;
using TestWebApiApplication.Shared;
using TestWebApiApplication.Shared.Models;

namespace TestHttpCaller.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private readonly HttpClient httpClient;

		private readonly ILogger<TestController> logger;

		private readonly AppSettings settings;

		public TestController(HttpClient httpClient, ILogger<TestController> logger, IOptions<AppSettings> options)
		{
			this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
		}

		[HttpGet]
		public async Task<HttpCallerResponse> Get(CancellationToken cancellationToken)
		{
			logger.LogInformation("Executing test HTTP call ...");

			var defaultRequestHeaders = httpClient.DefaultRequestHeaders
				.Select(x => (Header: x.Key, Values: x.Value))
				.SelectMany(x => x.Values.Select(y => (Header: x.Header, Value: y)))
				.ToList();

			var testResponse = await httpClient.GetFromJsonAsync<TestResponse>(settings.TestAppUri, cancellationToken);

			logger.LogInformation("Executed test HTTP call");

			return new HttpCallerResponse
			{
				Version = $"Test HttpCaller ({VersionHelper.GetVersion<TestController>()})",
				TestResponse = testResponse,
				DefaultClientHeaders = defaultRequestHeaders.Select(x => new HttpHeader(x)).ToList(),
			};
		}
	}
}
