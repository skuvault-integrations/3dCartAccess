using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ThreeDCartAccess.RestApi;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests.RestApi
{
	public class ThreeDCartProductsServiceTests
	{
		private static readonly Randomizer _randomizer = new Randomizer();

		[ Test ]
		public void Constructor_ShouldThrow_WhenConfigIsNull()
		{
			RestThreeDCartConfig config = null;

			Assert.Throws< ArgumentException >( () => new ThreeDCartProductsService( config, _randomizer.GetString(), TestHelper.GetMockLogger() ) );
		}
	}
}