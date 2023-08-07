using System;
using NUnit.Framework;
using ThreeDCartAccess.SoapApi;
using ThreeDCartAccess.SoapApi.Models.Configuration;

namespace ThreeDCartAccessTests.SoapApi
{
	public class ThreeDCartOrdersServiceTests
	{
		[ Test ]
		public void Constructor_ShouldThrow_WhenConfigIsNull()
		{
			ThreeDCartConfig config = null;

			Assert.Throws< ArgumentException >( () => new ThreeDCartOrdersService( config, TestHelper.CreateConsoleLogger() ) );
		}
	}
}