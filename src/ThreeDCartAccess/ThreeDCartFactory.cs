using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ThreeDCartAccess.Configuration;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.SoapApi;
using ThreeDCartAccess.SoapApi.Misc;
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

	/// <summary>
	/// This class represents the entry point of the ThreeDCartLibrary.
	/// </summary>
	public sealed class ThreeDCartFactory: IThreeDCartFactory
	{
		private readonly ISoapWebRequestServices _soapWebRequestServices;
		private string RestApiPrivateKey{ get; }
		private readonly ILogger _logger;

		public ThreeDCartFactory( ISoapWebRequestServices soapWebRequestServices, IOptions<ThreeDCartSettings> settings, ILogger logger )
		{
			this._soapWebRequestServices = soapWebRequestServices ?? throw new ArgumentNullException( nameof(soapWebRequestServices) );
			this.RestApiPrivateKey = settings.Value?.PrivateApiKey ?? throw new ArgumentNullException(nameof(settings));
			this._logger = logger;
		}

		public IThreeDCartProductsService CreateSoapProductsService( ThreeDCartConfig config )
		{
			return new ThreeDCartProductsService( config, this._logger );
		}

		public IThreeDCartOrdersService CreateSoapOrdersService( ThreeDCartConfig config )
		{
			return new ThreeDCartOrdersService( this._soapWebRequestServices, config, this._logger );
		}

		public RestApi.IThreeDCartProductsService CreateRestProductsService( RestThreeDCartConfig config )
		{
			config.SetPrivateKey( this.RestApiPrivateKey );
			return new RestApi.ThreeDCartProductsService( config, this._logger );
		}

		public RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestThreeDCartConfig config )
		{
			config.SetPrivateKey( this.RestApiPrivateKey );
			return new RestApi.ThreeDCartOrdersService( config, this._logger );
		}
	}
}