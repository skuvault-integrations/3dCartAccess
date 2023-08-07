using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Netco.Extensions;
using ServiceStack;
using SkuVault.Integrations.Core.Logging;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.RestApi.Models.Product.GetProducts;
using ThreeDCartAccess.RestApi.Models.Product.UpdateInventory;
using ThreeDCartProduct = ThreeDCartAccess.RestApi.Models.Product.GetProducts.ThreeDCartProduct;

namespace ThreeDCartAccess.RestApi
{
	public class ThreeDCartProductsService: ThreeDCartServiceBase, IThreeDCartProductsService
	{
		protected const int GetProductsLimit = 200;
		protected const int UpdateInventoryLimit = 100;

		public ThreeDCartProductsService( RestThreeDCartConfig config, string restApiPrivateKey, IIntegrationLogger logger ): base( config, restApiPrivateKey, logger )
		{
		}

		#region Get All Products
		public bool IsGetProducts()
		{
			try
			{
				var marker = this.GetMarker();
				var endpoint = EndpointsBuilder.GetProductsEnpoint( 1, GetProductsLimit );
				this.WebRequestServices.GetResponse< List< ThreeDCartProduct > >( endpoint, marker );
				return true;
			}
			catch( Exception )
			{
				return false;
			}
		}

		public List< ThreeDCartProduct > GetProducts()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartProduct >();
			this.GetCollection< ThreeDCartProduct >(
				marker,
				GetProductsLimit,
				( offset, pageSize ) => EndpointsBuilder.GetProductsEnpoint( offset, pageSize ),
				portion => result.AddRange( portion ) );
			return result;
		}

		public void GetProducts( Action< ThreeDCartProduct > processAction )
		{
			var marker = this.GetMarker();
			this.GetCollection< ThreeDCartProduct >(
				marker,
				GetProductsLimit,
				( offset, pageSize ) => EndpointsBuilder.GetProductsEnpoint( offset, pageSize ),
				portion =>
				{
					foreach( var product in portion )
						processAction( product );
				} );
		}

		public async Task< List< ThreeDCartProduct > > GetProductsAsync()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartProduct >();
			await this.GetCollectionAsync< ThreeDCartProduct >(
				marker,
				GetProductsLimit,
				( offset, pageSize ) => EndpointsBuilder.GetProductsEnpoint( offset, pageSize ),
				portion => result.AddRange( portion ) );
			return result;
		}

		public async Task GetProductsAsync( Action< ThreeDCartProduct > processAction )
		{
			var marker = this.GetMarker();
			await this.GetCollectionAsync< ThreeDCartProduct >(
				marker,
				GetProductsLimit,
				( offset, pageSize ) => EndpointsBuilder.GetProductsEnpoint( offset, pageSize ),
				portion =>
				{
					foreach( var product in portion )
						processAction( product );
				} );
		}

		public List< Models.Product.GetInventory.ThreeDCartProduct > GetInventory()
		{
			var marker = this.GetMarker();
			var result = new List< Models.Product.GetInventory.ThreeDCartProduct >();
			this.GetCollection< Models.Product.GetInventory.ThreeDCartProduct >(
				marker,
				GetProductsLimit,
				( offset, pageSize ) => EndpointsBuilder.GetProductsEnpoint( offset, pageSize ),
				portion => result.AddRange( portion ) );
			return result;
		}

		public async Task< List< Models.Product.GetInventory.ThreeDCartProduct > > GetInventoryAsync()
		{
			var marker = this.GetMarker();
			var result = new List< Models.Product.GetInventory.ThreeDCartProduct >();
			await this.GetCollectionAsync< Models.Product.GetInventory.ThreeDCartProduct >(
				marker,
				GetProductsLimit,
				( offset, pageSize ) => EndpointsBuilder.GetProductsEnpoint( offset, pageSize ),
				portion => result.AddRange( portion ) );
			return result;
		}

		public void UpdateInventory( Models.Product.UpdateInventory.ThreeDCartProduct inventory )
		{
			var marker = this.GetMarker();
			var endpoint = EndpointsBuilder.UpdateProductEnpoint( inventory.SKUInfo.CatalogID );
			var parentProduct = new ThreeDCartProductWithoutOptions( inventory );
			Resilience.Policies.Submit( this._logger ).Execute( () => this.WebRequestServices.PutData( endpoint, parentProduct.ToJson(), marker ) );
			this.UpdateOptionsInventory( inventory.SKUInfo.CatalogID, inventory.AdvancedOptionList, marker );
		}

		public async Task UpdateInventoryAsync( Models.Product.UpdateInventory.ThreeDCartProduct inventory )
		{
			var marker = this.GetMarker();
			var endpoint = EndpointsBuilder.UpdateProductEnpoint( inventory.SKUInfo.CatalogID );
			var parentProduct = new ThreeDCartProductWithoutOptions( inventory );
			await Resilience.Policies.SubmitAsync( this._logger ).ExecuteAsync( async () => await this.WebRequestServices.PutDataAsync( endpoint, parentProduct.ToJson(), marker ) );
			await this.UpdateOptionsInventoryAsync( inventory.SKUInfo.CatalogID, inventory.AdvancedOptionList, marker );
		}

		public void UpdateInventory( List< Models.Product.UpdateInventory.ThreeDCartProduct > inventory )
		{
			var marker = this.GetMarker();
			var endpoint = EndpointsBuilder.UpdateProductsEnpoint();
			var parts = inventory.Slice( UpdateInventoryLimit );
			foreach( var part in parts )
			{
				var parentProducts = part.Select( x => new ThreeDCartProductWithoutOptions( x ) ).ToList();
				Resilience.Policies.Submit( this._logger ).Execute( () => this.WebRequestServices.PutData( endpoint, parentProducts.ToJson(), marker ) );
				foreach( var threeDCartProduct in part )
				{
					this.UpdateOptionsInventory( threeDCartProduct.SKUInfo.CatalogID, threeDCartProduct.AdvancedOptionList, marker );
				}
			}
		}

		public async Task UpdateInventoryAsync( List< Models.Product.UpdateInventory.ThreeDCartProduct > inventory )
		{
			var marker = this.GetMarker();
			var endpoint = EndpointsBuilder.UpdateProductsEnpoint();
			var parts = inventory.Slice( UpdateInventoryLimit );
			foreach( var part in parts )
			{
				var parentProducts = part.Select( x => new ThreeDCartProductWithoutOptions( x ) ).ToList();
				await Resilience.Policies.SubmitAsync( this._logger ).ExecuteAsync( async () => await this.WebRequestServices.PutDataAsync( endpoint, parentProducts.ToJson(), marker ) );
				foreach( var threeDCartProduct in part )
				{
					await this.UpdateOptionsInventoryAsync( threeDCartProduct.SKUInfo.CatalogID, threeDCartProduct.AdvancedOptionList, marker );
				}
			}
		}

		private void UpdateOptionsInventory( long catalogId, List< ThreeDCartAdvancedOption > optionList, string marker )
		{
			if( optionList == null || optionList.Count == 0 )
				return;

			var endpoint = EndpointsBuilder.UpdateProductOptionsEnpoint( catalogId );
			Resilience.Policies.Submit( this._logger ).Execute( () => this.WebRequestServices.PutData( endpoint, optionList.ToJson(), marker ) );
		}

		private async Task UpdateOptionsInventoryAsync( long catalogId, List< ThreeDCartAdvancedOption > optionList, string marker )
		{
			if( optionList == null || optionList.Count == 0 )
				return;

			var endpoint = EndpointsBuilder.UpdateProductOptionsEnpoint( catalogId );
			await Resilience.Policies.SubmitAsync( this._logger ).ExecuteAsync( async () => await this.WebRequestServices.PutDataAsync( endpoint, optionList.ToJson(), marker ) );
		}
		#endregion
	}
}