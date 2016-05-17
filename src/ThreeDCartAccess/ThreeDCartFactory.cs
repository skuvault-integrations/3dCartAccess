using ThreeDCartAccess.SoapApi;
using ThreeDCartAccess.SoapApi.Models.Configuration;

namespace ThreeDCartAccess
{
	public interface IThreeDCartFactory
	{
		IThreeDCartProductsService CreateSoapProductsService( ThreeDCartConfig config );
		IThreeDCartOrdersService CreateSoapOrdersService( ThreeDCartConfig config );

		RestApi.IThreeDCartProductsService CreateRestProductsService( RestApi.Models.Configuration.ThreeDCartConfig config );
		RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestApi.Models.Configuration.ThreeDCartConfig config );
	}

	public class ThreeDCartFactory: IThreeDCartFactory
	{
		public string RestApiPrivateKey{ get; }

		public ThreeDCartFactory( string restApiPrivateKey = null )
		{
			this.RestApiPrivateKey = restApiPrivateKey;
		}

		public IThreeDCartProductsService CreateSoapProductsService( ThreeDCartConfig config )
		{
			return new ThreeDCartProductsService( config );
		}

		public IThreeDCartOrdersService CreateSoapOrdersService( ThreeDCartConfig config )
		{
			return new ThreeDCartOrdersService( config );
		}

		public RestApi.IThreeDCartProductsService CreateRestProductsService( RestApi.Models.Configuration.ThreeDCartConfig config )
		{
			config.SetPrivateKey( this.RestApiPrivateKey );
			return new RestApi.ThreeDCartProductsService( config );
		}

		public RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestApi.Models.Configuration.ThreeDCartConfig config )
		{
			config.SetPrivateKey( this.RestApiPrivateKey );
			return new RestApi.ThreeDCartOrdersService( config );
		}
	}
}