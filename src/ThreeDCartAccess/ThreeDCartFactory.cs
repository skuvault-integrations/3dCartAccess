using System.Collections.Generic;
using SkuVault.Integrations.Core.Helpers;
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
		public string RestApiPrivateKey{ get; private set; }

		public ThreeDCartFactory( string restApiPrivateKey = null )
		{
			this.RestApiPrivateKey = restApiPrivateKey;

			ValidationHelper.ThrowOnValidationErrors< ThreeDCartFactory >( GetValidationErrors() );
		}

		private IEnumerable< string > GetValidationErrors()
		{
			var validationErrors = new List<string>();
			if ( string.IsNullOrWhiteSpace( this.RestApiPrivateKey ) )
			{
				validationErrors.Add( $"{nameof( this.RestApiPrivateKey )} is null or white space" );
			}
			return validationErrors;
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
			config.SetPrivateKey( this.RestApiPrivateKey );
			return new RestApi.ThreeDCartProductsService( config );
		}

		public RestApi.IThreeDCartOrdersService CreateRestOrdersService( RestThreeDCartConfig config )
		{
			config.SetPrivateKey( this.RestApiPrivateKey );
			return new RestApi.ThreeDCartOrdersService( config );
		}
	}
}