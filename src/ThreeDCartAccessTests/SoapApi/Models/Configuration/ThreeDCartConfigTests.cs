using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ThreeDCartAccess.SoapApi.Models.Configuration;

namespace ThreeDCartAccessTests.SoapApi.Models.Configuration
{
	public class ThreeDCartConfigTests
	{
		private static readonly Randomizer _randomizer = new Randomizer();
		private readonly string validUserKey = _randomizer.GetString();
		private readonly string validStoreUrl = _randomizer.GetString();

		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenStoreUrlIsWhiteSpace_orNull( string storeUrl )
		{
			Assert.Throws< ArgumentException >( () => CreateThreeDCartConfig( storeUrl: storeUrl, this.validUserKey ) );
		}

		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenUserKeyIsWhiteSpace_orNull( string userKey )
		{
			Assert.Throws< ArgumentException >( () => CreateThreeDCartConfig( this.validStoreUrl, userKey: userKey ) );
		}

		[ TestCase( -13 ) ]
		[ TestCase( 13 ) ]
		public void Constructor_ShouldThrow_WhenTimeZoneIsOutOfRange( int timeZone )
		{
			Assert.Throws< ArgumentException >( () => CreateThreeDCartConfig( this.validStoreUrl, this.validUserKey, timeZone ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenAllParamsAreValid()
		{
			Assert.DoesNotThrow( () => CreateThreeDCartConfig( this.validStoreUrl, this.validUserKey ) );
		}

		[ TestCase( -12 ) ]
		[ TestCase( 12 ) ]
		public void Constructor_ShouldNotThrow_WhenTimeZoneIsOnTheBoundary( int timeZone )
		{
			Assert.DoesNotThrow( () => CreateThreeDCartConfig( this.validStoreUrl, this.validUserKey, timeZone ) );
		}

		[ TestCase( "https://www.SomeThing.com/\\", "something.com" ) ]
		public void Constructor_ShouldReturnStandardizedStoreUrl_WhenNonStandardStoreUrlIsPassed( string storeUrl, string storeUrlStandardized )
		{
			var result = CreateThreeDCartConfig( storeUrl, this.validUserKey );

			Assert.That( result.StoreUrl, Is.EqualTo( storeUrlStandardized ));
		}

		/// <summary>
		/// Instantiate ThreeDCartConfig
		/// </summary>
		/// <param name="storeUrl"></param>
		/// <param name="userKey"></param>
		/// <param name="timeZone">If null, then populate with a random valid value</param>
		/// <returns></returns>
		private static ThreeDCartConfig CreateThreeDCartConfig( string storeUrl, string userKey, int? timeZone = null )
		{
			return new ThreeDCartConfig( storeUrl, 
				userKey,
				timeZone ?? ( int )_randomizer.NextDecimal( -12, 12 ) );
		}
	}
}