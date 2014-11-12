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

		public ThreeDCartUpdatedInventory UpdateProductInventory( string productId, int quantity, bool isReplaceQty )
		{
			var result = this._webRequestServices.Submit< ThreeDCartUpdatedInventory >( this._config,
				() => this._service.updateProductInventory( this._config.StoreUrl, this._config.UserKey, productId, quantity, isReplaceQty, "" ) );
			return result;
		}

		public async Task< ThreeDCartUpdatedInventory > UpdateProductInventoryAsync( string productId, int quantity, bool isReplaceQty )
		{
			var result = await this._webRequestServices.SubmitAsync< ThreeDCartUpdatedInventory >( this._config,
				async () => ( await this._service.updateProductInventoryAsync( this._config.StoreUrl, this._config.UserKey, productId, quantity, isReplaceQty, "" ) ).Body.updateProductInventoryResult );
			return result;
		}
	}
}