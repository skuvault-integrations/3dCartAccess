using CuttingEdge.Conditions;

namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public sealed class ThreeDCartConfig
	{
		public string BaseUrl = "http://apirest.3dcart.com/3dCartWebAPI/v1";
		public string StoreUrl{ get; private set; }
		public string PrivateKey{ get; private set; }
		public string Token{ get; private set; }

		public ThreeDCartConfig( string storeUrl, string privateKey, string token )
		{
			Condition.Requires( storeUrl, "storeUrl" ).IsNotNullOrWhiteSpace();
			Condition.Requires( privateKey, "privateKey" ).IsNotNullOrWhiteSpace();
			Condition.Requires( token, "token" ).IsNotNullOrWhiteSpace();

			storeUrl = storeUrl.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" ).Replace( "www.", "" );

			this.StoreUrl = storeUrl;
			this.PrivateKey = privateKey;
			this.Token = token;
		}
	}
}