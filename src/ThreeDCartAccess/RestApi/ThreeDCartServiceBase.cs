﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkuVault.Integrations.Core.Helpers;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccess.RestApi
{
	public abstract class ThreeDCartServiceBase
	{
		protected readonly RestThreeDCartConfig Config;
		internal readonly WebRequestServices WebRequestServices;

		internal ThreeDCartServiceBase( RestThreeDCartConfig config )
		{
			this.Config = config;
			this.WebRequestServices = new WebRequestServices( config );

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
				portion = ActionPolicies.Get.Get( () => this.WebRequestServices.GetResponse< List< T > >( endpoint, marker ) );
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
				portion = await ActionPolicies.GetAsync.Get( async () => await this.WebRequestServices.GetResponseAsync< List< T > >( endpoint, marker ) );
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