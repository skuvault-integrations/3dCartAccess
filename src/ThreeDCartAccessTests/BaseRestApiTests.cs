using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests
{
	public abstract class BaseRestApiTests
	{
		protected string StoreUrl { get; set; }
		protected string Token { get; set; }
		protected int TimeZone { get; set; }

		protected RestThreeDCartConfig GetConfig() => new RestThreeDCartConfig( this.StoreUrl, this.Token, this.TimeZone );
	}
}