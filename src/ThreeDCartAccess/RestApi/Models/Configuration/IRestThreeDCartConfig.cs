namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public interface IRestThreeDCartConfig
	{
		string BaseUrl{ get; }
		string StoreUrl{ get; }
		string PrivateKey{ get; }
		string Token{ get; }
		int TimeZone{ get; }
		
		void SetPrivateKey( string privateKey );
	}
}