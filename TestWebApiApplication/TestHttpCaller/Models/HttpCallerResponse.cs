using System.Collections.Generic;
using TestWebApiApplication.Shared.Models;

namespace TestHttpCaller.Models
{
	public class HttpCallerResponse
	{
		public string Version { get; set; }

		public TestResponse TestResponse { get; set; }

		public IReadOnlyCollection<HttpHeader> DefaultClientHeaders { get; set; }
	}
}
