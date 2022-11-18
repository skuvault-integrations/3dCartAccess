using CuttingEdge.Conditions;

namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public abstract class RestThreeDCartConfigBase
	{
		public abstract string BaseUrl { get; }
		
		public string StoreUrl{ get; }
		public string PrivateKey{ get; private set; }
		public string Token{ get; }
		public int TimeZone{ get; }

		protected RestThreeDCartConfigBase( string storeUrl, string token, int timeZone )
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