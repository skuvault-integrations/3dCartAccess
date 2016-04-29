using CuttingEdge.Conditions;

namespace ThreeDCartAccess.V1.Models.Configuration
{
	public sealed class ThreeDCartConfig
	{
		public string StoreUrl{ get; private set; }
		public string UserKey{ get; private set; }
		public int TimeZone{ get; private set; }

		public ThreeDCartConfig( string storeUrl, string userKey, int timeZone )
		{
			Condition.Requires( storeUrl, "storeUrl" ).IsNotNullOrWhiteSpace();
			Condition.Requires( userKey, "userKey" ).IsNotNullOrWhiteSpace();
			Condition.Requires( timeZone, "timeZone" ).IsInRange( -12, 12 );

			storeUrl = storeUrl.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" ).Replace( "www.", "" );

			this.StoreUrl = storeUrl;
			this.UserKey = userKey;
			this.TimeZone = timeZone;
		}
	}
}