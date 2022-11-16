namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public sealed class RestThreeDCartConfigV2: RestThreeDCartConfigBase, IRestThreeDCartConfig
	{
		public string BaseUrl => "http://apirest.3dcart.com/3dCartWebAPI/v2";

		public RestThreeDCartConfigV2( string storeUrl, string token, int timeZone )
			: base( storeUrl, token, timeZone )
		{
		}
	}
}