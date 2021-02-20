using System.Diagnostics;

namespace TestWebApiApplication
{
	public static class VersionHelper
	{
		public static string GetVersion()
		{
			var assembly = typeof(VersionHelper).Assembly.Location;
			var versionInfo = FileVersionInfo.GetVersionInfo(assembly);
			return versionInfo.ProductVersion;
		}
	}
}
