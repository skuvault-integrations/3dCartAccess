using System;
using NUnit.Framework;
using ThreeDCartAccess.RestApi;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests.RestApi
{
	public class ThreeDCartProductsServiceTests
	{
		[ Test ]
		public void Constructor_ShouldThrow_WhenConfigIsNull()
		{
			RestThreeDCartConfig config = null;

			Assert.Throws< ArgumentException >( () => new ThreeDCartProductsService( config, TestHelper.CreateLogger() ) );
		}
	}
}