using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkuVault.Integrations.Core.Helpers;
using ThreeDCartAccess.ResiliencePolicies;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccess.RestApi
{
	public abstract class ThreeDCartServiceBase
	{
		protected readonly RestThreeDCartConfig Config;
		protected readonly ILogger Logger;
		internal readonly RestWebRequestServices _restWebRequestServices;

		internal ThreeDCartServiceBase( RestThreeDCartConfig config, ILogger logger )
		{
			this.Config = config;
			this.Logger = logger;
			this._restWebRequestServices = new RestWebRequestServices( config, logger );

			ValidationHelper.ThrowOnValidationErrors< RestThreeDCartConfig >( GetValidationErrors() );
		}

		private IEnumerable< string > GetValidationErrors()
		{
			var validationErrors = new List<string>();
			if ( this.Config == null )
			{
				validationErrors.Add( $"{nameof( this.Config )} is null" );
			}
			
			return validationErrors;
		}

		protected string GetMarker()
		{
			return Guid.NewGuid().ToString();
		}

		protected void GetCollection< T >( string marker, int pageSize, Func< int, int, string > endpointFunc, Action< List< T > > processAction )
		{
			List< T > portion = null;
			for( var i = 1;; i += portion.Count )
			{
				var endpoint = endpointFunc( i, pageSize );
				portion = PolicyRegistry.CreateGetPolicy().Execute(
					( _, _ ) => this._restWebRequestServices.GetResponse< List< T > >( endpoint, marker ),
					new PolicyContext( this.Logger ),
					new CancellationToken() );
				if( portion == null || portion.Count == 0 )
					break;

				processAction( portion );
				
				if( portion.Count < pageSize )
					break;
				
				Task.Delay( 100 ).Wait();
			}
		}

		protected async Task GetCollectionAsync< T >( string marker, int pageSize, Func< int, int, string > endpointFunc, Action< List< T > > processAction )
		{
			List< T > portion = null;
			for( var i = 1;; i += portion.Count )
			{
				var endpoint = endpointFunc( i, pageSize );
				portion = await PolicyRegistry.CreateGetAsyncPolicy().ExecuteAsync(
					async ( _, _ ) => await this._restWebRequestServices.GetResponseAsync< List< T > >( endpoint, marker ),
					new PolicyContext( this.Logger ),
					new CancellationToken() );
				if( portion == null || portion.Count == 0 )
					break;

				processAction( portion );
				
				if( portion.Count < pageSize )
					break;
				
				await Task.Delay( 100 );
			}
		}
	}
}