using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;
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
		public bool IsGetProducts()
		{
			try
			{
				var marker = this.GetMarker();
				var endpoint = EndpointsBuilder.GetProductsEnpoint( 1, BatchSize );
				this.WebRequestServices.GetResponse< List< ThreeDCartProduct > >( endpoint, marker );
				return true;
			}
			catch( Exception )
			{
				return false;
			}
		}

		public List< ThreeDCartProduct > GetAllProducts()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartProduct >();
			this.GetCollection< ThreeDCartProduct >( marker, offset => EndpointsBuilder.GetProductsEnpoint( offset, BatchSize ), portion => result.AddRange( portion ) );
			return result;
		}

		public void GetAllProducts( Action< ThreeDCartProduct > processAction )
		{
			var marker = this.GetMarker();
			this.GetCollection< ThreeDCartProduct >( marker, offset => EndpointsBuilder.GetProductsEnpoint( offset, BatchSize ), portion =>
			{
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}

		public async Task< List< ThreeDCartProduct > > GetAllProductsAsync()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartProduct >();
			await this.GetCollectionAsync< ThreeDCartProduct >( marker, offset => EndpointsBuilder.GetProductsEnpoint( offset, BatchSize ), portion => result.AddRange( portion ) );
			return result;
		}

		public async Task GetAllProductsAsync( Action< ThreeDCartProduct > processAction )
		{
			var marker = this.GetMarker();
			await this.GetCollectionAsync< ThreeDCartProduct >( marker, offset => EndpointsBuilder.GetProductsEnpoint( offset, BatchSize ), portion =>
			{
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}

		public void UpdateInventory( List< ThreeDCartProductForUpdatingInventory > inventory )
		{
			var marker = this.GetMarker();
			foreach( var product in inventory )
			{
				var endpoint = EndpointsBuilder.UpdateProductsEnpoint( product.SKUInfo.CatalogID );
				ActionPolicies.Submit.Do( () => this.WebRequestServices.PutData( endpoint, product.ToJson(), marker ) );
			}
		}

		public async Task UpdateInventoryAsync( List< ThreeDCartProductForUpdatingInventory > inventory )
		{
			var marker = this.GetMarker();
			foreach( var product in inventory )
			{
				var endpoint = EndpointsBuilder.UpdateProductsEnpoint( product.SKUInfo.CatalogID );
				await ActionPolicies.SubmitAsync.Do( async () => await this.WebRequestServices.PutDataAsync( endpoint, product.ToJson(), marker ) );
			}
		}
		#endregion
	}
}