using System;
using NUnit.Framework;
using ThreeDCartAccess;

namespace ThreeDCartAccessTests
{
	public class ThreeDCartFactoryTests
	{
		[ TestCase( "" ) ]
		[ TestCase( " " ) ]
		[ TestCase( "	" ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenPrivateKeyIsWhiteSpace_orNull( string privateKey )
		{
			Assert.Throws< ArgumentException >( () => new ThreeDCartFactory( privateKey ) );
		}
	}
}