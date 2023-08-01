using System;
using NUnit.Framework;
using ThreeDCartAccess.SoapApi;
using ThreeDCartAccess.SoapApi.Models.Configuration;

namespace ThreeDCartAccessTests.SoapApi
{
	public class ThreeDCartProductsServiceTests
	{
		[ Test ]
		public void Constructor_ShouldThrow_WhenConfigIsNull()
		{
			ThreeDCartConfig config = null;

			Assert.Throws< ArgumentException >( () => new ThreeDCartProductsService( config, null ) );
		}
	}
}