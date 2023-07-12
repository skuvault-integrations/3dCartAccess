using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests.RestApi.Models.Configuration
{
	public class RestThreeDCartConfigTests
	{
		private static readonly Randomizer _randomizer = new Randomizer();
		private readonly string validToken = _randomizer.GetString();
		private readonly string validStoreUrl = _randomizer.GetString();

		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenStoreUrlIsWhiteSpace_orNull( string storeUrl )
		{
			Assert.Throws< ArgumentException >( () => CreateRestThreeDCartConfig( storeUrl: storeUrl, this.validToken ) );
		}

		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenTokenIsWhiteSpace_orNull( string token )
		{
			Assert.Throws< ArgumentException >( () => CreateRestThreeDCartConfig( this.validStoreUrl, token: token ) );
		}

		[ TestCase( -13 ) ]
		[ TestCase( 13 ) ]
		public void Constructor_ShouldThrow_WhenTimeZoneIsOutOfRange( int timeZone )
		{
			Assert.Throws< ArgumentException >( () => CreateRestThreeDCartConfig( this.validStoreUrl, this.validToken, timeZone ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenAllParamsAreValid()
		{
			Assert.DoesNotThrow( () => CreateRestThreeDCartConfig( this.validStoreUrl, this.validToken ) );
		}

		[ TestCase( -12 ) ]
		[ TestCase( 12 ) ]
		public void Constructor_ShouldNotThrow_WhenTimeZoneIsOnTheBoundary( int timeZone )
		{
			Assert.DoesNotThrow( () => CreateRestThreeDCartConfig( this.validStoreUrl, this.validToken, timeZone ) );
		}

		[ TestCase( "https://www.SomeThing.com/\\", "something.com" ) ]
		public void Constructor_ShouldReturnStandardizedStoreUrl_WhenNonStandardStoreUrlIsPassed( string storeUrl, string storeUrlStandardized )
		{
			var result = CreateRestThreeDCartConfig( storeUrl, this.validToken );

			Assert.That( result.StoreUrl, Is.EqualTo( storeUrlStandardized ));
		}

		/// <summary>
		/// Instantiate RestThreeDCartConfig
		/// </summary>
		/// <param name="storeUrl"></param>
		/// <param name="token"></param>
		/// <param name="timeZone">If null, then populate with a random valid value</param>
		/// <returns></returns>
		private static RestThreeDCartConfig CreateRestThreeDCartConfig( string storeUrl, string token, int? timeZone = null )
		{
			return new RestThreeDCartConfig( storeUrl, 
				token,
				timeZone ?? ( int )_randomizer.NextDecimal( -12, 12 ) );
		}
	}
}