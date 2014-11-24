using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.Models.Configuration;
using ThreeDCartAccess.Models.Product;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess
{
	public class ThreeDCartProductsService: IThreeDCartProductsService
	{
		private readonly ThreeDCartConfig _config;
		private readonly cartAPISoapClient _service;
		private readonly WebRequestServices _webRequestServices;
		private const int _batchSize = 100;

		public ThreeDCartProductsService( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public IEnumerable< ThreeDCartProduct > GetProducts()
		{
			var result = new List< ThreeDCartProduct >();
			var productsCount = this.GetProductsCount();
			for( var i = 1; i < productsCount; i += _batchSize )
			{
				var portion = this._webRequestServices.Get< ThreeDCartProducts >( this._config,
					() => this._service.getProduct( this._config.StoreUrl, this._config.UserKey, _batchSize, i, "", "" ) );
				result.AddRange( portion.Products );
				if( portion.Products.Count != _batchSize )
					return result;
			}

			return result;
		}

		public async Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync()
		{
			var result = new List< ThreeDCartProduct >();
			var productsCount = await this.GetProductsCountAsync();
			for( var i = 1; i < productsCount; i += _batchSize )
			{
				var portion = await this._webRequestServices.GetAsync< ThreeDCartProducts >( this._config,
					async () => ( await this._service.getProductAsync( this._config.StoreUrl, this._config.UserKey, _batchSize, i, "", "" ) ).Body.getProductResult );
				result.AddRange( portion.Products );
				if( portion.Products.Count != _batchSize )
					return result;
			}

			return result;
		}

		public int GetProductsCount()
		{
			var result = this._webRequestServices.Get< ThreeDCartProductsCount >( this._config,
				() => this._service.getProductCount( this._config.StoreUrl, this._config.UserKey, "" ) );
			return result.Quantity;
		}

		public async Task< int > GetProductsCountAsync()
		{
			var result = await this._webRequestServices.GetAsync< ThreeDCartProductsCount >( this._config,
				async () => ( await this._service.getProductCountAsync( this._config.StoreUrl, this._config.UserKey, "" ) ).Body.getProductCountResult );
			return result.Quantity;
		}

		public ThreeDCartUpdateInventory UpdateInventory( ThreeDCartUpdateInventory inventory )
		{
			var result = this._webRequestServices.Submit< ThreeDCartUpdateInventory >( this._config,
				() => this._service.updateProductInventory( this._config.StoreUrl, this._config.UserKey, inventory.ProductId, inventory.Quantity, inventory.IsReplaceQty, "" ) );
			return result;
		}

		public async Task< ThreeDCartUpdateInventory > UpdateInventoryAsync( ThreeDCartUpdateInventory inventory )
		{
			var result = await this._webRequestServices.SubmitAsync< ThreeDCartUpdateInventory >( this._config,
				async () => ( await this._service.updateProductInventoryAsync( this._config.StoreUrl, this._config.UserKey, inventory.ProductId, inventory.Quantity, inventory.IsReplaceQty, "" ) )
					.Body.updateProductInventoryResult );
			return result;
		}

		public IEnumerable< ThreeDCartUpdateInventory > UpdateInventory( IEnumerable< ThreeDCartUpdateInventory > inventory )
		{
			var result = new List< ThreeDCartUpdateInventory >();
			foreach( var inv in inventory )
			{
				var response = this.UpdateInventory( inv );
				if( response != null )
					result.Add( response );
			}
			return result;
		}

		public async Task< IEnumerable< ThreeDCartUpdateInventory > > UpdateInventoryAsync( IEnumerable< ThreeDCartUpdateInventory > inventory )
		{
			var result = new List< ThreeDCartUpdateInventory >();
			foreach( var inv in inventory )
			{
				var response = await this.UpdateInventoryAsync( inv );
				if( response != null )
					result.Add( response );
			}
			return result;
		}
	}
}