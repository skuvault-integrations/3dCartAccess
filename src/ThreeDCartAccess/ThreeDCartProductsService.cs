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

		public ThreeDCartProductsService( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public IEnumerable< ThreeDCartProduct > GetProducts()
		{
			var result = this._webRequestServices.Get< ThreeDCartProducts >( this._config,
				() => this._service.getProduct( this._config.StoreUrl, this._config.UserKey, 100, 1, "", "" ) );
			return result.Products;
		}

		public async Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync()
		{
			var result = await this._webRequestServices.GetAsync< ThreeDCartProducts >( this._config,
				async () => ( await this._service.getProductAsync( this._config.StoreUrl, this._config.UserKey, 100, 1, "", "" ) ).Body.getProductResult );
			return result.Products;
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