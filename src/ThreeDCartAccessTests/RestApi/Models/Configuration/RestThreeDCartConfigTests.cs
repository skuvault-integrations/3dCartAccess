using System;
using NUnit.Framework;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests.RestApi.Models.Configuration
{
	public class RestThreeDCartConfigTests
	{
		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		[ TestCase( "https://www.\\" ) ]
		public void Constructor_ShouldThrow_WhenStoreUrlIsWhiteSpace_orNull( string storeUrl )
		{
			Assert.Throws< ArgumentException >( () => CreateRestThreeDCartConfig( storeUrl: storeUrl, TestHelper.validToken ) );
		}

		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenTokenIsWhiteSpace_orNull( string token )
		{
			Assert.Throws< ArgumentException >( () => CreateRestThreeDCartConfig( TestHelper.validStoreUrl, token: token ) );
		}

		[ TestCase( -13 ) ]
		[ TestCase( 13 ) ]
		public void Constructor_ShouldThrow_WhenTimeZoneIsOutOfRange( int timeZone )
		{
			Assert.Throws< ArgumentException >( () => CreateRestThreeDCartConfig( TestHelper.validStoreUrl, TestHelper.validToken, timeZone ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenAllParamsAreValid()
		{
			Assert.DoesNotThrow( () => CreateRestThreeDCartConfig( TestHelper.validStoreUrl, TestHelper.validToken ) );
		}

		[ TestCase( -12 ) ]
		[ TestCase( 12 ) ]
		public void Constructor_ShouldNotThrow_WhenTimeZoneIsOnTheBoundary( int timeZone )
		{
			Assert.DoesNotThrow( () => CreateRestThreeDCartConfig( TestHelper.validStoreUrl, TestHelper.validToken, timeZone ) );
		}

		[ TestCase( "https://www.SomeThing.com/\\", "something.com" ) ]
		public void Constructor_ShouldReturnStandardizedStoreUrl_WhenNonStandardStoreUrlIsPassed( string storeUrl, string storeUrlStandardized )
		{
			var result = CreateRestThreeDCartConfig( storeUrl, TestHelper.validToken );

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
				timeZone ?? TestHelper.validTimeZone );
		}
	}
}