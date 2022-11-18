using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests
{
	public abstract class BaseRestApiTests
	{
		protected string StoreUrl { get; set; }
		protected string Token { get; set; }
		protected int TimeZone { get; set; }

		protected RestThreeDCartConfigBase GetConfig( ThreeDCartConfigVersion configVersion ) =>
			configVersion == ThreeDCartConfigVersion.V1
				? (RestThreeDCartConfigBase)new RestThreeDCartConfigV1( this.StoreUrl, this.Token, this.TimeZone )
				: new RestThreeDCartConfigV2( this.StoreUrl, this.Token, this.TimeZone );
	}
}