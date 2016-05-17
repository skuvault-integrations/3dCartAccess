using CuttingEdge.Conditions;

namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public sealed class ThreeDCartConfig
	{
		public string BaseUrl = "http://apirest.3dcart.com/3dCartWebAPI/v1";
		public string StoreUrl{ get; private set; }
		public string PrivateKey{ get; private set; }
		public string Token{ get; private set; }
		public int TimeZone{ get; private set; }

		public ThreeDCartConfig( string storeUrl, string token, int timeZone )
		{
			Condition.Requires( storeUrl, "storeUrl" ).IsNotNullOrWhiteSpace();
			Condition.Requires( token, "token" ).IsNotNullOrWhiteSpace();
			Condition.Requires( timeZone, "timeZone" ).IsInRange( -12, 12 );

			storeUrl = storeUrl.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" ).Replace( "www.", "" );
			this.StoreUrl = storeUrl;
			this.Token = token;
			this.TimeZone = timeZone;
		}

		internal void SetPrivateKey( string privateKey )
		{
			Condition.Requires( privateKey, "privateKey" ).IsNotNullOrWhiteSpace();
			this.PrivateKey = privateKey;
		}
	}
}