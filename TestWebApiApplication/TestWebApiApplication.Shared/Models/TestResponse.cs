using System.Collections.Generic;

namespace TestWebApiApplication.Shared.Models
{
	public class TestResponse
	{
		public string Version { get; set; }

		public IReadOnlyCollection<HttpHeader> RequestHeaders { get; set; }
	}
}
