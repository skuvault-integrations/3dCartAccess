using System.Collections.Generic;
using SkuVault.Integrations.Core.Helpers;

namespace ThreeDCartAccess.SoapApi.Models.Configuration
{
	public sealed class ThreeDCartConfig
	{
		private readonly string storeUrlRaw;
		public string StoreUrl => this.storeUrlRaw.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" ).Replace( "www.", "" );
		public string UserKey{ get; private set; }
		public int TimeZone{ get; private set; }

		public ThreeDCartConfig( string storeUrl, string userKey, int timeZone )
		{
			this.storeUrlRaw = storeUrl;
			this.UserKey = userKey;
			this.TimeZone = timeZone;

			ValidationHelper.ThrowOnValidationErrors< ThreeDCartConfig >( GetValidationErrors() );
		}

		private IEnumerable< string > GetValidationErrors()
		{
			var validationErrors = new List<string>();
			if ( string.IsNullOrWhiteSpace( this.storeUrlRaw ) )
			{
				validationErrors.Add( $"{nameof( this.storeUrlRaw )} is null or white space" );
			}
			if ( string.IsNullOrWhiteSpace( this.UserKey ) )
			{
				validationErrors.Add( $"{nameof( this.UserKey )} is null or white space" );
			}
			if ( this.TimeZone is < -12 or > 12 )
			{
				validationErrors.Add( $"{nameof( this.TimeZone )} is less than -12 or greater than 12" );
			}
			
			return validationErrors;
		}
	}
}