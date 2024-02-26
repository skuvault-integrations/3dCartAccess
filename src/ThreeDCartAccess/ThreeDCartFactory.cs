using Microsoft.Extensions.Options;
using SkuVault.Integrations.Core.Logging;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccess
{
	public interface IThreeDCartFactory
	{

		RestApi.IThreeDCartProductsService CreateRestProductsService( RestThreeDCartConfig config );
		RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestThreeDCartConfig config );
	}

	public class ThreeDCartFactory: IThreeDCartFactory
	{
		private string RestApiPrivateKey{ get; }
		private readonly IIntegrationLogger _logger;

		public ThreeDCartFactory( IOptions< ThreeDCartSettings > settings, IIntegrationLogger logger )
		{
			this.RestApiPrivateKey = settings?.Value?.PrivateApiKey;
			this._logger = logger;
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