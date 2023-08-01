using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkuVault.Integrations.Core.Helpers;
using ThreeDCartAccess.ResiliencePolicies;
using ThreeDCartAccess.SoapApi.Misc;
using ThreeDCartAccess.SoapApi.Models.Configuration;
using ThreeDCartAccess.SoapApi.Models.Product;
using ThreeDCartAccess.ThreeDCartAdvancedService;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess.SoapApi
{
	public class ThreeDCartProductsService: IThreeDCartProductsService
	{
		private readonly ThreeDCartConfig _config;
		private readonly cartAPISoapClient _service;
		private readonly cartAPIAdvancedSoapClient _advancedService;
		private readonly SoapWebRequestServices _soapWebRequestServices;
		private const int BatchSize = 100;
		private const int BatchSizeAdvanced = 500;
		private ILogger _logger;

		public ThreeDCartProductsService( ThreeDCartConfig config, ILogger logger )
		{
			this._config = config;
			this._logger = logger;
			this._service = new cartAPISoapClient();
			this._advancedService = new cartAPIAdvancedSoapClient();
			this._soapWebRequestServices = new SoapWebRequestServices(logger);

			ValidationHelper.ThrowOnValidationErrors< ThreeDCartProductsService >( GetValidationErrors() );
		}

		private IEnumerable< string > GetValidationErrors()
		{
			var validationErrors = new List<string>();
			if ( this._config == null )
			{
				validationErrors.Add( $"{nameof( this._config )} is null" );
			}
			
			return validationErrors;
		}

		/// <summary>Verify that can get products. Will return true on success and throw on failure.</summary>
		public bool IsGetProducts()
		{
			var parsedResult = this._soapWebRequestServices.Execute< ThreeDCartProducts >( "IsGetProducts", this._config,
				() => this._service.getProduct( this._config.StoreUrl, this._config.UserKey, BatchSize, 1, "", "" ) );
			return true;
		}

		public IEnumerable< ThreeDCartProduct > GetProducts()
		{
			var result = new List< ThreeDCartProduct >();
			for( var i = 1;; i += BatchSize )
			{
				var portion = PolicyRegistry.CreateGetPolicy().Execute(
					( _, _ ) => this._soapWebRequestServices.Execute< ThreeDCartProducts >( "GetProducts", this._config,
						() => this._service.getProduct( this._config.StoreUrl, this._config.UserKey, BatchSize, i, "", "" ) ),
					new PolicyContext( this._logger ),
					new CancellationToken() );
				if( portion == null )
					break;

				result.AddRange( portion.Products );
				if( portion.Products.Count != BatchSize )
					break;
			}

			return result;
		}

		public async Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync()
		{
			var result = new List< ThreeDCartProduct >();
			for( var i = 1;; i += BatchSize )
			{
				var portion = await PolicyRegistry.CreateGetAsyncPolicy().ExecuteAsync(
					async ( _, _ ) => await this._soapWebRequestServices.ExecuteAsync< ThreeDCartProducts >( "GetProductsAsync", this._config,
						async () => ( await this._service.getProductAsync( this._config.StoreUrl, this._config.UserKey, BatchSize, i, "", "" ) ).Body.getProductResult ),
					new PolicyContext( this._logger ),
					new CancellationToken() );
				if( portion == null )
					break;

				result.AddRange( portion.Products );
				if( portion.Products.Count != BatchSize )
					break;
			}

			return result;
		}

		/// <summary>Verify that can get inventory. Will return true on success and throw on failure.</summary>
		public bool IsGetInventory()
		{
			var parsedResult = this.GetInventoryPageOrAllPages( 1 );
			return true;
		}

		public IEnumerable< ThreeDCartInventory > GetInventory()
		{
			var result = new List< ThreeDCartInventory >();
			for( var i = 1;; i += BatchSizeAdvanced )
			{
				var portion = PolicyRegistry.CreateGetPolicy().Execute(
					( _, _ ) => this.GetInventoryPageOrAllPages( i ),
					new PolicyContext( this._logger ),
					new CancellationToken() );
				if( portion == null )
					break;
				if( portion.IsFullInventory )
				{
					result = portion.Inventory;
					break;
				}
				result.AddRange( portion.Inventory );
				if( portion.Inventory.Count != BatchSizeAdvanced )
					break;
			}

			return result;
		}

		public async Task< IEnumerable< ThreeDCartInventory > > GetInventoryAsync()
		{
			var result = new List< ThreeDCartInventory >();
			for( var i = 1;; i += BatchSizeAdvanced )
			{
				var portion = await PolicyRegistry.CreateGetAsyncPolicy().ExecuteAsync(
					async ( _, _ ) => await this.GetInventoryPageOrAllPagesAsync( i ),
					new PolicyContext( this._logger ),
					new CancellationToken() );
				if( portion == null )
					break;
				if( portion.IsFullInventory )
				{
					result = portion.Inventory;
					break;
				}
				result.AddRange( portion.Inventory );
				if( portion.Inventory.Count != BatchSizeAdvanced )
					break;
			}

			return result;
		}

		private ThreeDCartInventories GetInventoryPageOrAllPages( int page )
		{
			try
			{
				var sql = ScriptsBuilder.GetInventory( BatchSizeAdvanced, page );
				var parsedResult = this._soapWebRequestServices.Execute< ThreeDCartInventories >( "GetInventoryPage", this._config,
					() => this._advancedService.runQuery( this._config.StoreUrl, this._config.UserKey, sql, "" ) );
				return parsedResult;
			}
			catch( Exception ex )
			{
				// will try to get all data if it doesn't not support ROW_NUMBER() 
				if( !ex.Message.Contains( "Syntax error (missing operator) in query expression 'ROW_NUMBER() OVER(ORDER BY p.catalogid)'" ) )
					throw;
			}
			var sql2 = ScriptsBuilder.GetInventory();
			var parsedResult2 = this._soapWebRequestServices.Execute< ThreeDCartInventories >( "GetInventoryAllPages", this._config,
				() => this._advancedService.runQuery( this._config.StoreUrl, this._config.UserKey, sql2, "" ) );
			parsedResult2.IsFullInventory = true;
			return parsedResult2;
		}

		private async Task< ThreeDCartInventories > GetInventoryPageOrAllPagesAsync( int page )
		{
			try
			{
				var sql = ScriptsBuilder.GetInventory( BatchSizeAdvanced, page );
				var parsedResult = await this._soapWebRequestServices.ExecuteAsync< ThreeDCartInventories >( "GetInventoryPageAsync", this._config,
					async () => ( await this._advancedService.runQueryAsync( this._config.StoreUrl, this._config.UserKey, sql, "" ) ).Body.runQueryResult );
				return parsedResult;
			}
			catch( Exception ex )
			{
				// will try to get all data if it doesn't not support ROW_NUMBER() 
				if( !ex.Message.Contains( "Syntax error (missing operator) in query expression 'ROW_NUMBER() OVER(ORDER BY p.catalogid)'" ) )
					throw;
			}
			var sql2 = ScriptsBuilder.GetInventory();
			var parsedResult2 = await this._soapWebRequestServices.ExecuteAsync< ThreeDCartInventories >( "GetInventoryAllPagesAsync", this._config,
				async () => ( await this._advancedService.runQueryAsync( this._config.StoreUrl, this._config.UserKey, sql2, "" ) ).Body.runQueryResult );
			parsedResult2.IsFullInventory = true;
			return parsedResult2;
		}

		public IEnumerable< ThreeDCartUpdateInventory > UpdateInventory( IEnumerable< ThreeDCartUpdateInventory > inventory, bool updateProductTotalStock = false )
		{
			var result = new List< ThreeDCartUpdateInventory >();
			var productsWithOptions = new List< string >();
			foreach( var inv in inventory )
			{
				ThreeDCartUpdateInventory response;
				if( string.IsNullOrEmpty( inv.OptionCode ) )
					response = this.UpdateProductInventory( inv );
				else
				{
					response = this.UpdateProductOptionInventory( inv );
					if( !productsWithOptions.Contains( inv.ProductId ) )
						productsWithOptions.Add( inv.ProductId );
				}
				if( response != null )
					result.Add( response );
			}

			if( !updateProductTotalStock || productsWithOptions.Count == 0 )
				return result;

			var updatedInventory = this.GetInventory().ToList();
			foreach( var product in productsWithOptions )
			{
				var sum = updatedInventory.Where( x => x.IsProductOption && x.ProductId == product && x.OptionStock > 0 ).Sum( x => x.OptionStock );
				var response = this.UpdateProductInventory( new ThreeDCartUpdateInventory { ProductId = product, NewQuantity = sum } );
				if( response != null )
					result.Add( response );
			}

			return result;
		}

		public async Task< IEnumerable< ThreeDCartUpdateInventory > > UpdateInventoryAsync( IEnumerable< ThreeDCartUpdateInventory > inventory, bool updateProductTotalStock = false )
		{
			var result = new List< ThreeDCartUpdateInventory >();
			var productsWithOptions = new List< string >();
			foreach( var inv in inventory )
			{
				ThreeDCartUpdateInventory response;
				if( string.IsNullOrEmpty( inv.OptionCode ) )
					response = await this.UpdateProductInventoryAsync( inv );
				else
				{
					response = await this.UpdateProductOptionInventoryAsync( inv );
					if( !productsWithOptions.Contains( inv.ProductId ) )
						productsWithOptions.Add( inv.ProductId );
				}
				if( response != null )
					result.Add( response );
			}

			if( !updateProductTotalStock || productsWithOptions.Count == 0 )
				return result;

			var updatedInventory = this.GetInventory().ToList();
			foreach( var product in productsWithOptions )
			{
				var sum = updatedInventory.Where( x => x.IsProductOption && x.ProductId == product && x.OptionStock > 0 ).Sum( x => x.OptionStock );
				var response = await this.UpdateProductInventoryAsync( new ThreeDCartUpdateInventory { ProductId = product, NewQuantity = sum } );
				if( response != null )
					result.Add( response );
			}

			return result;
		}

		private ThreeDCartUpdateInventory UpdateProductInventory( ThreeDCartUpdateInventory inventory )
		{
			var result = PolicyRegistry.CreateSubmitPolicy().Execute(
				( _, _ ) => this._soapWebRequestServices.Execute< ThreeDCartUpdateInventory >( "UpdateProductInventory", this._config,
					() => this._service.updateProductInventory( this._config.StoreUrl, this._config.UserKey, inventory.ProductId, inventory.NewQuantity, true, "" ) ),
				new PolicyContext( this._logger ),
				new CancellationToken() );
			return result;
		}

		private async Task< ThreeDCartUpdateInventory > UpdateProductInventoryAsync( ThreeDCartUpdateInventory inventory )
		{
			var result = await PolicyRegistry.CreateSubmitAsyncPolicy().ExecuteAsync(
				async ( _, _ ) => await this._soapWebRequestServices.ExecuteAsync< ThreeDCartUpdateInventory >( "UpdateProductInventoryAsync", this._config,
					async () => ( await this._service.updateProductInventoryAsync( this._config.StoreUrl, this._config.UserKey, inventory.ProductId, inventory.NewQuantity, true, "" ) )
						.Body.updateProductInventoryResult ),
				new PolicyContext( this._logger ),
				new CancellationToken() );
			return result;
		}

		private ThreeDCartUpdateInventory UpdateProductOptionInventory( ThreeDCartUpdateInventory inventory )
		{
			var sql = ScriptsBuilder.UpdateProductOptionInventory( inventory.NewQuantity, inventory.OptionCode );
			var result = PolicyRegistry.CreateSubmitPolicy().Execute(
				( _, _ ) => this._soapWebRequestServices.Execute< ThreeDCartUpdatedOptionInventory >( "UpdateProductOptionInventory", this._config,
					() => this._advancedService.runQuery( this._config.StoreUrl, this._config.UserKey, sql, "" ) ),
				new PolicyContext( this._logger ),
				new CancellationToken() );
			return inventory;
		}

		private async Task< ThreeDCartUpdateInventory > UpdateProductOptionInventoryAsync( ThreeDCartUpdateInventory inventory )
		{
			var sql = ScriptsBuilder.UpdateProductOptionInventory( inventory.NewQuantity, inventory.OptionCode );
			var result = await PolicyRegistry.CreateSubmitAsyncPolicy().ExecuteAsync(
				async ( _, _ ) => await this._soapWebRequestServices.ExecuteAsync< ThreeDCartUpdatedOptionInventory >( "UpdateProductOptionInventoryAsync", this._config,
					async () => ( await this._advancedService.runQueryAsync( this._config.StoreUrl, this._config.UserKey, sql, "" ) ).Body.runQueryResult ),
				new PolicyContext( this._logger ),
				new CancellationToken() );
			return inventory;
		}
	}
}