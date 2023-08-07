using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThreeDCartAccess.DependencyInjection;

namespace ThreeDCartAccessTests
{
	public static class TestHelper
	{
		public static ILogger< string > CreateConsoleLogger() => LoggerFactory.Create( ConfigureConsoleLoggerBuilder() ).CreateLogger< string >();

		/// <summary>
		/// Create service provider with console logger
		/// </summary>
		/// <param name="apiPrivateKey"></param>
		/// <returns></returns>
		internal static ServiceProvider CreateServiceProvider( string apiPrivateKey )
		{
			var services = new ServiceCollection();
			services.Add3DCartServices( apiPrivateKey, ConfigureConsoleLoggerBuilder() );
			return services.BuildServiceProvider();
		}

		private static Action< ILoggingBuilder > ConfigureConsoleLoggerBuilder() => builder => builder.AddConsole();
	}
}