using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.Models.Configuration;
using ThreeDCartAccess.Models.Product;
using ThreeDCartAccess.ThreeDCartAdvancedService;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess
{
	public class ThreeDCartProductsService: IThreeDCartProductsService
	{
		private readonly ThreeDCartConfig _config;
		private readonly cartAPISoapClient _service;
		private readonly cartAPIAdvancedSoapClient _advancedService;
		private readonly WebRequestServices _webRequestServices;
		private const int _batchSize = 100;

		public ThreeDCartProductsService( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
			this._advancedService = new cartAPIAdvancedSoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public IEnumerable< ThreeDCartProduct > GetProducts()
		{
			var result = new List< ThreeDCartProduct >();
			for( var i = 1;; i += _batchSize )
			{
				var portion = this._webRequestServices.Get< ThreeDCartProducts >( this._config,
					() => this._service.getProduct( this._config.StoreUrl, this._config.UserKey, _batchSize, i, "", "" ) );
				if( portion == null )
					break;

				result.AddRange( portion.Products );
				if( portion.Products.Count != _batchSize )
					break;
			}

			return result;
		}

		public IEnumerable< ThreeDCartInventory > GetInventory()
		{
			const string sql = "select p.catalogid, p.id, p.name, p.stock, p.show_out_stock, o.AO_Code, o.AO_Sufix, o.AO_Name, o.AO_Cost, o.AO_Stock from products AS p " +
			                   "LEFT JOIN options_Advanced AS o on p.catalogid = o.ProductID";
			var result = this._webRequestServices.Get< ThreeDCartInventories >( this._config,
				() => this._advancedService.runQuery( this._config.StoreUrl, this._config.UserKey, sql, "" ) );
			return result.Inventory;
		}

		public async Task< IEnumerable< ThreeDCartInventory > > GetInventoryAsync()
		{
			const string sql = "select p.catalogid, p.id, p.name, p.stock, o.AO_Code, o.AO_Sufix, o.AO_Name, o.AO_Cost, o.AO_Stock from products AS p " +
			                   "LEFT JOIN options_Advanced AS o on p.catalogid = o.ProductID";
			var result = await this._webRequestServices.GetAsync< ThreeDCartInventories >( this._config,
				async () => ( await this._advancedService.runQueryAsync( this._config.StoreUrl, this._config.UserKey, sql, "" ) ).Body.runQueryResult );
			return result.Inventory;
		}

		public async Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync()
		{
			var result = new List< ThreeDCartProduct >();
			for( var i = 1;; i += _batchSize )
			{
				var portion = await this._webRequestServices.GetAsync< ThreeDCartProducts >( this._config,
					async () => ( await this._service.getProductAsync( this._config.StoreUrl, this._config.UserKey, _batchSize, i, "", "" ) ).Body.getProductResult );
				if( portion == null )
					break;

				result.AddRange( portion.Products );
				if( portion.Products.Count != _batchSize )
					break;
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

		public ThreeDCartUpdateInventory UpdateInventory( ThreeDCartUpdateInventory inventory )
		{
			var result = string.IsNullOrEmpty( inventory.OptionCode ) ? this.UpdateProductInventory( inventory ) : UpdateProductOptionInventory( inventory );
			return result;
		}

		public async Task< ThreeDCartUpdateInventory > UpdateInventoryAsync( ThreeDCartUpdateInventory inventory )
		{
			var result = string.IsNullOrEmpty( inventory.OptionCode ) ? await this.UpdateProductInventoryAsync( inventory ) : await UpdateProductOptionInventoryAsync( inventory );
			return result;
		}

		private ThreeDCartUpdateInventory UpdateProductInventory( ThreeDCartUpdateInventory inventory )
		{
			var result = this._webRequestServices.Submit< ThreeDCartUpdateInventory >( this._config,
				() => this._service.updateProductInventory( this._config.StoreUrl, this._config.UserKey, inventory.ProductId, inventory.NewQuantity, true, "" ) );
			return result;
		}

		private async Task< ThreeDCartUpdateInventory > UpdateProductInventoryAsync( ThreeDCartUpdateInventory inventory )
		{
			var result = await this._webRequestServices.SubmitAsync< ThreeDCartUpdateInventory >( this._config,
				async () => ( await this._service.updateProductInventoryAsync( this._config.StoreUrl, this._config.UserKey, inventory.ProductId, inventory.NewQuantity, true, "" ) )
					.Body.updateProductInventoryResult );
			return result;
		}

		private string GetSqlForUpdateProductOptionInventory( ThreeDCartUpdateInventory inventory )
		{
			var sql = string.Format( "UPDATE options_Advanced SET AO_Stock = {0} WHERE AO_Sufix = '{1}'", inventory.NewQuantity, inventory.OptionCode );
			if( !inventory.UpdateProductTotalStock )
				return sql;

			var diff = inventory.NewQuantity > 0
				? inventory.OldQuantity > 0 ? inventory.NewQuantity - inventory.OldQuantity : inventory.NewQuantity
				: inventory.OldQuantity > 0 ? -inventory.OldQuantity : 0;

			var sql2 = string.Format( "UPDATE products SET stock = stock + {0} WHERE id = '{1}'", diff, inventory.ProductId );
			return string.Format( "{0}|;;|{1}", sql, sql2 );
		}

		private ThreeDCartUpdateInventory UpdateProductOptionInventory( ThreeDCartUpdateInventory inventory )
		{
			var sql = this.GetSqlForUpdateProductOptionInventory( inventory );
			var result = this._webRequestServices.Submit< ThreeDCartUpdatedOptionInventory >( this._config,
				() => this._advancedService.runQuery( this._config.StoreUrl, this._config.UserKey, sql, "" ) );
			return inventory;
		}

		private async Task< ThreeDCartUpdateInventory > UpdateProductOptionInventoryAsync( ThreeDCartUpdateInventory inventory )
		{
			var sql = this.GetSqlForUpdateProductOptionInventory( inventory );
			var result = await this._webRequestServices.SubmitAsync< ThreeDCartUpdatedOptionInventory >( this._config,
				async () => ( await this._advancedService.runQueryAsync( this._config.StoreUrl, this._config.UserKey, sql, "" ) ).Body.runQueryResult );
			return inventory;
		}
	}
}