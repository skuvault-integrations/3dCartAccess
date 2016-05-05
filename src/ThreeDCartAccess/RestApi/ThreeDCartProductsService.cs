using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.RestApi.Models.Product;

namespace ThreeDCartAccess.RestApi
{
	public class ThreeDCartProductsService: ThreeDCartServiceBase, IThreeDCartProductsService
	{
		public ThreeDCartProductsService( ThreeDCartConfig config ): base( config )
		{
		}

		#region Get All Products
		public List< ThreeDCartProduct > GetAllProducts()
		{
			var result = new List< ThreeDCartProduct >();
			this.GetAllProducts( portion => result.AddRange( portion ) );
			return result;
		}

		public void GetAllProducts( Action< ThreeDCartProduct > processAction )
		{
			this.GetAllProducts( portion =>
			{
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}

		private void GetAllProducts( Action< List< ThreeDCartProduct > > processAction )
		{
			var marker = this.GetMarker();

			for( var i = 1;; i += BatchSize )
			{
				var portion = ActionPolicies.Get.Get( () => this.WebRequestServices.GetResponse< List< ThreeDCartProduct > >( EndpointsBuilder.GetAllProductsEnpoint( i, BatchSize ), marker ) );
				if( portion == null )
					break;

				processAction( portion );
				if( portion.Count != BatchSize )
					break;
			}
		}

		public async Task< List< ThreeDCartProduct > > GetAllProductsAsync()
		{
			var result = new List< ThreeDCartProduct >();
			await this.GetAllProductsAsync( portion => result.AddRange( portion ) );
			return result;
		}

		public async Task GetAllProductsAsync( Action< ThreeDCartProduct > processAction )
		{
			await this.GetAllProductsAsync( portion =>
			{
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}

		private async Task GetAllProductsAsync( Action< List< ThreeDCartProduct > > processAction )
		{
			var marker = this.GetMarker();

			for( var i = 1;; i += BatchSize )
			{
				var portion = await ActionPolicies.GetAsync.Get( async () =>
					await this.WebRequestServices.GetResponseAsync< List< ThreeDCartProduct > >( EndpointsBuilder.GetAllProductsEnpoint( i, BatchSize ), marker ) );
				if( portion == null )
					break;

				processAction( portion );
				if( portion.Count != BatchSize )
					break;
			}
		}
		#endregion
	}
}