using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Netco.Extensions;
using SkuVault.Integrations.Core.Helpers;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.SoapApi.Misc;
using ThreeDCartAccess.SoapApi.Models.Configuration;
using ThreeDCartAccess.SoapApi.Models.Order;
using ThreeDCartAccess.ThreeDCartAdvancedService;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess.SoapApi
{
	public class ThreeDCartOrdersService: IThreeDCartOrdersService
	{
		private static readonly CultureInfo _culture = new CultureInfo( "en-US" );
		private readonly ThreeDCartConfig _config;
		private readonly ILogger _logger;
		private readonly cartAPISoapClient _service;
		private readonly cartAPIAdvancedSoapClient _advancedService;
		private readonly WebRequestServices _webRequestServices;
		private const int _batchSize = 100;

		public ThreeDCartOrdersService( ThreeDCartConfig config, ILogger logger )
		{
			this._config = config;
			this._logger = logger;
			this._service = new cartAPISoapClient();
			this._advancedService = new cartAPIAdvancedSoapClient();
			this._webRequestServices = new WebRequestServices( this._logger );

			ValidationHelper.ThrowOnValidationErrors< ThreeDCartOrdersService >( GetValidationErrors() );
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

		/// <summary>Verify that can get new orders. Will return true on success and throw on failure.</summary>
		public bool IsGetNewOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null )
		{
			var startDate = this.GetDate( startDateUtc ?? DateTime.UtcNow.AddDays( -30 ) );
			var endDate = this.GetDate( endDateUtc ?? DateTime.UtcNow );
			var parsedResult = this._webRequestServices.Execute< ThreeDCartOrders >( "IsGetNewOrders", this._config,
				() =>
				{
					var ordersResponse = this._service.getOrder( this._config.StoreUrl, this._config.UserKey, _batchSize, 1, true, "", "", startDate, endDate, "" );
					ErrorHelpers.ThrowIfError( ordersResponse, this._config.StoreUrl, this._logger );
					return ordersResponse;
				} );
			return true;
		}

		public List< ThreeDCartOrder > GetNewOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = new List< ThreeDCartOrder >();
			for( var i = 1;; i += _batchSize )
			{
				var portion = ActionPolicies.Get( this._logger ).Get(
					() => this._webRequestServices.Execute< ThreeDCartOrders >( "GetNewOrders", this._config,
						() => this._service.getOrder( this._config.StoreUrl, this._config.UserKey, _batchSize, i, true, "", "", startDate, endDate, "" ) ) );
				if( portion == null )
					break;

				var filtered = this.SetTimeZoneAndFilter( portion.Orders, startDateUtc, endDateUtc );
				result.AddRange( filtered );
				if( portion.Orders.Count != _batchSize )
					break;
			}

			return result;
		}

		public async Task< List< ThreeDCartOrder > > GetNewOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = new List< ThreeDCartOrder >();
			for( var i = 1;; i += _batchSize )
			{
				var portion = await ActionPolicies.GetAsync( this._logger ).Get(
					async () => await this._webRequestServices.ExecuteAsync< ThreeDCartOrders >( "GetNewOrdersAsync", this._config,
						async () => {
								var ordersResponse = ( await this._service.getOrderAsync( this._config.StoreUrl, this._config.UserKey, _batchSize, i, true, "", "", startDate, endDate, "" ) ).Body.getOrderResult;
							
								ErrorHelpers.ThrowIfError( ordersResponse, this._config.StoreUrl, this._logger );
							
								return ordersResponse;
						} ) );
				
				if ( portion == null )
					break;	

				var filtered = this.SetTimeZoneAndFilter( portion.Orders, startDateUtc, endDateUtc );
				result.AddRange( filtered );
				if( portion.Orders.Count != _batchSize )
					break;
			}

			return result;
		}

		public List< ThreeDCartOrder > GetOrdersByNumber( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = invoiceNumbers.Select( this.GetOrderByNumber ).Where( order => order != null );
			var result = this.SetTimeZoneAndFilter( orders, startDateUtc, endDateUtc );
			return result;
		}

		public async Task< List< ThreeDCartOrder > > GetOrdersByNumberAsync( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = await invoiceNumbers.ProcessInBatchAsync( 50, async invoiceNumber =>
			{
				var order = await this.GetOrderByNumberAsync( invoiceNumber.Trim() );
				return order;
			} );

			var result = this.SetTimeZoneAndFilter( orders, startDateUtc, endDateUtc );
			return result;
		}

		public ThreeDCartOrder GetOrderByNumber( string invoiceNumber )
		{
			var portion = ActionPolicies.Get( this._logger ).Get(
				() => this._webRequestServices.Execute< ThreeDCartOrders >( "GetOrder", this._config,
					() => this._service.getOrder( this._config.StoreUrl, this._config.UserKey, 1, 1, false, invoiceNumber, "", "", "", "" ) ) );

			return portion == null || portion.Orders.Count == 0 || portion.Orders[ 0 ].InvoiceNumber != invoiceNumber ? null : portion.Orders[ 0 ];
		}

		public async Task< ThreeDCartOrder > GetOrderByNumberAsync( string invoiceNumber )
		{									
			var portion = await ActionPolicies.GetAsync( this._logger ).Get(
				async () => await this._webRequestServices.ExecuteAsync< ThreeDCartOrders >( "GetOrderAsync", this._config,
					async () =>
					{
						var ordersResponse = ( await this._service.getOrderAsync( this._config.StoreUrl, this._config.UserKey, 1, 1, false, invoiceNumber, "", "", "", "" ) ).Body.getOrderResult;
						ErrorHelpers.ThrowIfError( ordersResponse, this._config.StoreUrl, this._logger );
						return ordersResponse;
					}
					)
				);

			return portion == null || portion.Orders.Count == 0 || portion.Orders[ 0 ].InvoiceNumber != invoiceNumber ? null : portion.Orders[ 0 ];
		}

		public int GetOrdersCount( DateTime? startDateUtc = null, DateTime? endDateUtc = null )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = this.GetOrdersCount( startDate, endDate );
			return result;
		}

		public async Task< int > GetOrdersCountAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = await this.GetOrdersCountAsync( startDate, endDate );
			return result;
		}

		private int GetOrdersCount( string startDateUtc, string endDateUtc )
		{
			var result = ActionPolicies.Get( this._logger ).Get(
				() => this._webRequestServices.Execute< ThreeDCartOrdersCount >( "GetOrdersCount", this._config,
					() => this._service.getOrderCount( this._config.StoreUrl, this._config.UserKey, false, "", "", startDateUtc, endDateUtc, "" ) ) );
			return result.Quantity;
		}

		private async Task< int > GetOrdersCountAsync( string startDateUtc, string endDateUtc )
		{
			var result = await ActionPolicies.GetAsync( this._logger ).Get(
				async () => await this._webRequestServices.ExecuteAsync< ThreeDCartOrdersCount >( "GetOrdersCountAsync", this._config,
					async () => ( await this._service.getOrderCountAsync( this._config.StoreUrl, this._config.UserKey, false, "", "", startDateUtc, endDateUtc, "" ) ).Body.getOrderCountResult ) );
			return result.Quantity;
		}

		public List< ThreeDCartOrderStatus > GetOrderStatuses()
		{
			var sql = ScriptsBuilder.GetOrderStatuses();
			var result = ActionPolicies.Get( this._logger ).Get(
				() => this._webRequestServices.Execute< ThreeDCartOrderStatuses >( "GetOrderStatuses", this._config,
					() => this._advancedService.runQuery( this._config.StoreUrl, this._config.UserKey, sql, "" ) ) );
			return result.Statuses;
		}

		public async Task< List< ThreeDCartOrderStatus > > GetOrderStatusesAsync()
		{
			var sql = ScriptsBuilder.GetOrderStatuses();
			var result = await ActionPolicies.GetAsync( this._logger ).Get(
				async () => await this._webRequestServices.ExecuteAsync< ThreeDCartOrderStatuses >( "GetOrderStatusesAsync", this._config,
					async () => ( await this._advancedService.runQueryAsync( this._config.StoreUrl, this._config.UserKey, sql, "" ) ).Body.runQueryResult ) );
			return result.Statuses;
		}

		private List< ThreeDCartOrder > SetTimeZoneAndFilter( IEnumerable< ThreeDCartOrder > orders, DateTime startDateUtc, DateTime endDateUtc )
		{
			var result = new List< ThreeDCartOrder >();
			foreach( var order in orders )
			{
				if( order.OrderStatus == ThreeDCartOrderStatusEnum.NotCompleted )
					continue;

				order.TimeZone = this._config.TimeZone;
				if( order.DateTimeCreatedUtc >= startDateUtc && order.DateTimeCreatedUtc <= endDateUtc ||
				    order.DateTimeUpdatedUtc >= startDateUtc && order.DateTimeUpdatedUtc <= endDateUtc )
					result.Add( order );
			}
			return result;
		}

		private string GetDate( DateTime? dateInUtc )
		{
			return dateInUtc == null ? string.Empty : string.Format( _culture, "{0:MM/dd/yyyy}", dateInUtc.Value.AddHours( this._config.TimeZone ) );
		}
	}
}