using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework.Internal;
using SkuVault.Integrations.Core.Logging;
using ThreeDCartAccess.DependencyInjection;

namespace ThreeDCartAccessTests
{
	public static class TestHelper
	{
		private static readonly Randomizer _randomizer = new Randomizer();
		internal static readonly string validUserKey = _randomizer.GetString();
		internal static readonly string validStoreUrl = _randomizer.GetString();
		internal static readonly string validToken = _randomizer.GetString();
		internal static readonly int validTimeZone = ( int )_randomizer.NextDecimal( -12, 12 );

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