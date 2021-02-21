using System.Diagnostics;

namespace TestWebApiApplication.Shared
{
	public static class VersionHelper
	{
		public static string GetVersion<TAnchor>()
		{
			var assembly = typeof(TAnchor).Assembly.Location;
			var versionInfo = FileVersionInfo.GetVersionInfo(assembly);
			return versionInfo.ProductVersion;
		}
	}
}
