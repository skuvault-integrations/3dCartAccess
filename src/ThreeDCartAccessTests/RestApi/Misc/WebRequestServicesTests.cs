using System;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SkuVault.Integrations.Core.Logging;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests.RestApi.Misc
{
	public class WebRequestServicesTests
	{
		private static readonly Randomizer _randomizer = new Randomizer();
		private RestThreeDCartConfig validConfig;

		[ OneTimeSetUp ]
		public void Init()
		{
			var validToken = _randomizer.GetString();
			var validStoreUrl = _randomizer.GetString();
			var validTimeZone = 0;
			this.validConfig = new RestThreeDCartConfig( validStoreUrl, validToken, validTimeZone );
		}
		
		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenRestApiPrivateKeyIsWhiteSpace_orNull( string restApiPrivateKey )
		{
			var mockLogger = Substitute.For< IIntegrationLogger >();

			Assert.Throws< ArgumentException >( () => new WebRequestServices( this.validConfig, restApiPrivateKey, mockLogger ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenRestApiPrivateKeyIsNotBlank()
		{
			var mockLogger = Substitute.For< IIntegrationLogger >();

			Assert.DoesNotThrow( () => new WebRequestServices( this.validConfig, "someApiPrivateKey", mockLogger ) );
		}
	}
}