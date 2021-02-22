namespace TestWebApiApplication.Shared.Models
{
	public class HttpHeader
	{
		public string Header { get; set; }

		public string Value { get; set; }

		public HttpHeader()
		{
		}

		public HttpHeader((string Header, string Value) data)
		{
			Header = data.Header;
			Value = data.Value;
		}
	}
}
