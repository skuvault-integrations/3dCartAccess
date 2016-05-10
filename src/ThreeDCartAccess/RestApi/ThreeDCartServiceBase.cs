using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccess.RestApi
{
	public abstract class ThreeDCartServiceBase
	{
		protected readonly ThreeDCartConfig Config;
		internal readonly WebRequestServices WebRequestServices;
		protected const int BatchSize = 100;

		protected ThreeDCartServiceBase( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this.Config = config;
			this.WebRequestServices = new WebRequestServices( config );
		}

		protected string GetMarker()
		{
			return Guid.NewGuid().ToString();
		}

		protected void GetCollection< T >( string marker, Func< int, string > endpointFunc, Action< List< T > > processAction )
		{
			for( var i = 1;; i += BatchSize )
			{
				var endpoint = endpointFunc( i );
				var portion = ActionPolicies.Get.Get( () => this.WebRequestServices.GetResponse< List< T > >( endpoint, marker ) );
				if( portion == null )
					break;

				processAction( portion );
				if( portion.Count != BatchSize )
					break;
			}
		}

		protected async Task GetCollectionAsync< T >( string marker, Func< int, string > endpointFunc, Action< List< T > > processAction )
		{
			for( var i = 1;; i += BatchSize )
			{
				var endpoint = endpointFunc( i );
				var portion = await ActionPolicies.GetAsync.Get( async () => await this.WebRequestServices.GetResponseAsync< List< T > >( endpoint, marker ) );
				if( portion == null )
					break;

				processAction( portion );
				if( portion.Count != BatchSize )
					break;
			}
		}
	}
}