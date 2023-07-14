using System.Collections.Generic;
using SkuVault.Integrations.Core.Helpers;

namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public sealed class RestThreeDCartConfig
	{
		public string BaseUrl => "http://apirest.3dcart.com/3dCartWebAPI/v2";

		public string StoreUrl{ get; private set; }
		public string PrivateKey{ get; private set; }
		public string Token{ get; }
		public int TimeZone{ get; }

		public RestThreeDCartConfig( string storeUrlRaw, string token, int timeZone )
		{
			this.StoreUrl = StandardizeStoreUrl( storeUrlRaw );
			this.Token = token;
			this.TimeZone = timeZone;

			ValidationHelper.ThrowOnValidationErrors< RestThreeDCartConfig >( GetValidationErrors() );
		}

		private static string StandardizeStoreUrl( string storeUrlRaw )
		{
			return storeUrlRaw?.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" ).Replace( "www.", "" ) ?? string.Empty;
		}

		private IEnumerable< string > GetValidationErrors()
		{
			var validationErrors = new List<string>();
			if ( string.IsNullOrWhiteSpace( this.StoreUrl ) )
			{
				validationErrors.Add( $"{nameof( this.StoreUrl )} is null or white space" );
			}
			if ( string.IsNullOrWhiteSpace( this.Token ) )
			{
				validationErrors.Add( $"{nameof( this.Token )} is null or white space" );
			}
			if ( this.TimeZone is < -12 or > 12 )
			{
				validationErrors.Add( $"{nameof( this.TimeZone )} is less than -12 or greater than 12" );
			}
			
			return validationErrors;
		}

		internal void SetPrivateKey( string privateKey )
		{
			this.PrivateKey = privateKey;
			ValidationHelper.ThrowOnValidationErrors< ThreeDCartFactory >( GetValidationErrorsForSetPrivateKey() );
		}

		private IEnumerable< string > GetValidationErrorsForSetPrivateKey()
		{
			var validationErrors = new List<string>();
			if ( string.IsNullOrWhiteSpace( this.PrivateKey ) )
			{
				validationErrors.Add( $"{nameof( this.PrivateKey )} is null or white space" );
			}
			return validationErrors;
		}
	}
}