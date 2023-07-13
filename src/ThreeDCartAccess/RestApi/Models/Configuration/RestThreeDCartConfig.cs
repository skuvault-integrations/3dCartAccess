using System.Collections.Generic;
using SkuVault.Integrations.Core.Helpers;

namespace ThreeDCartAccess.RestApi.Models.Configuration
{
	public sealed class RestThreeDCartConfig
	{
		public string BaseUrl => "http://apirest.3dcart.com/3dCartWebAPI/v2";

		private readonly string storeUrlRaw;
		public string StoreUrl => this.storeUrlRaw.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" ).Replace( "www.", "" );

		public string PrivateKey{ get; private set; }
		public string Token{ get; }
		public int TimeZone{ get; }

		public RestThreeDCartConfig( string storeUrl, string token, int timeZone )
		{
			this.storeUrlRaw = storeUrl;
			this.Token = token;
			this.TimeZone = timeZone;

			ValidationHelper.ThrowOnValidationErrors< RestThreeDCartConfig >( GetValidationErrors() );
		}

		private IEnumerable< string > GetValidationErrors()
		{
			var validationErrors = new List<string>();
			if ( string.IsNullOrWhiteSpace( this.storeUrlRaw ) )
			{
				validationErrors.Add( $"{nameof( this.storeUrlRaw )} is null or white space" );
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