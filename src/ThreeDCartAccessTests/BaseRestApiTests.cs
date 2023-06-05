using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests
{
	public abstract class BaseRestApiTests
	{
		protected string StoreUrl { get; set; }
		protected string Token { get; set; }
		protected int TimeZone { get; set; }

		protected RestThreeDCartConfigV2 GetConfig() => new RestThreeDCartConfigV2( this.StoreUrl, this.Token, this.TimeZone );
	}
}