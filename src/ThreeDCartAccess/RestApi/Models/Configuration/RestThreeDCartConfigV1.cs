namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public sealed class RestThreeDCartConfigV1: RestThreeDCartConfigBase
	{
		public override string BaseUrl => "http://apirest.3dcart.com/3dCartWebAPI/v1";

		public RestThreeDCartConfigV1( string storeUrl, string token, int timeZone )
			: base( storeUrl, token, timeZone )
		{
		}
	}
}