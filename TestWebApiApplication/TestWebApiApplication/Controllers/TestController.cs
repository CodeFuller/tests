using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TestWebApiApplication.Shared;
using TestWebApiApplication.Shared.Models;

namespace TestWebApiApplication.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		private readonly ILogger<TestController> logger;

		public TestController(IHttpContextAccessor httpContextAccessor, ILogger<TestController> logger)
		{
			this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		public TestResponse Get()
		{
			logger.LogInformation("Processing test HTTP request ...");

			var httpContext = httpContextAccessor.HttpContext;
			var requestHeaders = httpContext.Request.Headers
				.Select(x => (Header: x.Key, Values: x.Value))
				.SelectMany(x => x.Values.Select(y => (Header: x.Header, Value: y)))
				.ToList();

			logger.LogInformation("Request headers: {@RequestHeaders}", requestHeaders);

			return new TestResponse
			{
				Version = $"TestWebApiApplication ({VersionHelper.GetVersion<TestController>()})",
				RequestHeaders = requestHeaders.Select(x => new HttpHeader
				{
					Header = x.Header,
					Value = x.Value,
				}).ToList(),
			};
		}
	}
}
