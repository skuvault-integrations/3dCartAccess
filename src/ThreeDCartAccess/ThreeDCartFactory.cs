using Microsoft.Extensions.Options;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.SoapApi;
using ThreeDCartAccess.SoapApi.Models.Configuration;

namespace ThreeDCartAccess
{
	public interface IThreeDCartFactory
	{
		IThreeDCartProductsService CreateSoapProductsService( ThreeDCartConfig config );
		IThreeDCartOrdersService CreateSoapOrdersService( ThreeDCartConfig config );

		RestApi.IThreeDCartProductsService CreateRestProductsService( RestThreeDCartConfig config );
		RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestThreeDCartConfig config );
	}

	public class ThreeDCartFactory: IThreeDCartFactory
	{
		public string RestApiPrivateKey{ get; }

		public ThreeDCartFactory( IOptions< SkuVaultDeveloperSettings > settings )
		{
			this.RestApiPrivateKey = settings?.Value?.PrivateApiKey;
		}

		public IThreeDCartProductsService CreateSoapProductsService( ThreeDCartConfig config )
		{
			return new ThreeDCartProductsService( config );
		}

		public IThreeDCartOrdersService CreateSoapOrdersService( ThreeDCartConfig config )
		{
			return new ThreeDCartOrdersService( config );
		}

		public RestApi.IThreeDCartProductsService CreateRestProductsService( RestThreeDCartConfig config )
		{
			return new RestApi.ThreeDCartProductsService( config, this.RestApiPrivateKey );
		}

		public RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestThreeDCartConfig config )
		{
			return new RestApi.ThreeDCartOrdersService( config, this.RestApiPrivateKey );
		}
	}
}