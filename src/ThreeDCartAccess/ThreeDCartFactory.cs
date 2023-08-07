using Microsoft.Extensions.Logging;
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
		private string RestApiPrivateKey{ get; }
		private readonly ILogger< string > _logger;

		public ThreeDCartFactory( IOptions< ThreeDCartSettings > settings, ILogger< string > logger )
		{
			this.RestApiPrivateKey = settings?.Value?.PrivateApiKey;
			this._logger = logger;
		}

		public IThreeDCartProductsService CreateSoapProductsService( ThreeDCartConfig config )
		{
			return new ThreeDCartProductsService( config, this._logger );
		}

		public IThreeDCartOrdersService CreateSoapOrdersService( ThreeDCartConfig config )
		{
			return new ThreeDCartOrdersService( config, this._logger );
		}

		public RestApi.IThreeDCartProductsService CreateRestProductsService( RestThreeDCartConfig config )
		{
			return new RestApi.ThreeDCartProductsService( config, this.RestApiPrivateKey, this._logger );
		}

		public RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestThreeDCartConfig config )
		{
			return new RestApi.ThreeDCartOrdersService( config, this.RestApiPrivateKey, this._logger );
		}
	}
}