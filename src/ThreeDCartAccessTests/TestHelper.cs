using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SkuVault.Integrations.Core.Logging;
using ThreeDCartAccess.DependencyInjection;

namespace ThreeDCartAccessTests
{
	public static class TestHelper
	{
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

		internal static IIntegrationLogger GetMockLogger()
		{
			return Substitute.For< IIntegrationLogger >();
		}
	}
}