using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using Netco.Extensions;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.Models.Configuration;
using ThreeDCartAccess.Models.Order;
using ThreeDCartAccess.ThreeDCartAdvancedService;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess
{
	public class ThreeDCartOrdersService: IThreeDCartOrdersService
	{
		private static readonly CultureInfo _culture = new CultureInfo( "en-US" );
		private readonly ThreeDCartConfig _config;
		private readonly cartAPISoapClient _service;
		private readonly cartAPIAdvancedSoapClient _advancedService;
		private readonly WebRequestServices _webRequestServices;
		private const int _batchSize = 100;

		public ThreeDCartOrdersService( ThreeDCartConfig config, bool retryOnlyOneTime )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
			this._advancedService = new cartAPIAdvancedSoapClient();
			var actionPolicies = retryOnlyOneTime ? new ActionPolicies( 1 ) : new ActionPolicies() ;
			this._webRequestServices = new WebRequestServices( actionPolicies );
		}

		public IEnumerable< ThreeDCartOrder > GetNewOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = new List< ThreeDCartOrder >();
			for( var i = 1;; i += _batchSize )
			{
				var portion = this._webRequestServices.Get< ThreeDCartOrders >( this._config,
					() => this._service.getOrder( this._config.StoreUrl, this._config.UserKey, _batchSize, i, true, "", "", startDate, endDate, "" ) );
				if( portion == null )
					break;

				var filtered = this.FilterNotCompletedOrdersIfNeeded( portion.Orders, includeNotCompleted );
				result.AddRange( filtered );
				if( portion.Orders.Count != _batchSize )
					break;
			}

			result = this.SetTimeZoneAndFilterByDate( result, startDateUtc, endDateUtc );
			return result;
		}

		public async Task< IEnumerable< ThreeDCartOrder > > GetNewOrdersAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = new List< ThreeDCartOrder >();
			for( var i = 1;; i += _batchSize )
			{
				var portion = await this._webRequestServices.GetAsync< ThreeDCartOrders >( this._config,
					async () => ( await this._service.getOrderAsync( this._config.StoreUrl, this._config.UserKey, _batchSize, i, true, "", "", startDate, endDate, "" ) ).Body.getOrderResult );
				if( portion == null )
					break;

				var filtered = this.FilterNotCompletedOrdersIfNeeded( portion.Orders, includeNotCompleted );
				result.AddRange( filtered );
				if( portion.Orders.Count != _batchSize )
					break;
			}

			result = this.SetTimeZoneAndFilterByDate( result, startDateUtc, endDateUtc );
			return result;
		}

		public IEnumerable< ThreeDCartOrder > GetOrders( IEnumerable< string > invoiceNumbers, DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false )
		{
			var orders = invoiceNumbers.Select( this.GetOrder ).Where( order => order != null );
			var filtered = this.FilterNotCompletedOrdersIfNeeded( orders, includeNotCompleted );
			var result = this.SetTimeZoneAndFilterByDate( filtered, startDateUtc, endDateUtc );
			return result;
		}

		public async Task< IEnumerable< ThreeDCartOrder > > GetOrdersAsync( IEnumerable< string > invoiceNumbers, DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false )
		{
			var orders = await invoiceNumbers.ProcessInBatchAsync( 50, invoiceNumber =>
			{
				var order = this.GetOrderAsync( invoiceNumber );
				return order;
			} );

			var filtered = this.FilterNotCompletedOrdersIfNeeded( orders, includeNotCompleted );
			var result = this.SetTimeZoneAndFilterByDate( filtered, startDateUtc, endDateUtc );
			return result;
		}

		public ThreeDCartOrder GetOrder( string invoiceNumber )
		{
			var portion = this._webRequestServices.Get< ThreeDCartOrders >( this._config,
				() => this._service.getOrder( this._config.StoreUrl, this._config.UserKey, 1, 1, false, invoiceNumber, "", "", "", "" ) );

			return portion == null || portion.Orders.Count == 0 || portion.Orders[ 0 ].InvoiceNumber != invoiceNumber ? null : portion.Orders[ 0 ];
		}

		public async Task< ThreeDCartOrder > GetOrderAsync( string invoiceNumber )
		{
			var portion = await this._webRequestServices.GetAsync< ThreeDCartOrders >( this._config,
				async () => ( await this._service.getOrderAsync( this._config.StoreUrl, this._config.UserKey, 1, 1, false, invoiceNumber, "", "", "", "" ) ).Body.getOrderResult );

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
			var result = this._webRequestServices.Get< ThreeDCartOrdersCount >( this._config,
				() => this._service.getOrderCount( this._config.StoreUrl, this._config.UserKey, false, "", "", startDateUtc, endDateUtc, "" ) );
			return result.Quantity;
		}

		private async Task< int > GetOrdersCountAsync( string startDateUtc, string endDateUtc )
		{
			var result = await this._webRequestServices.GetAsync< ThreeDCartOrdersCount >( this._config,
				async () => ( await this._service.getOrderCountAsync( this._config.StoreUrl, this._config.UserKey, false, "", "", startDateUtc, endDateUtc, "" ) ).Body.getOrderCountResult );
			return result.Quantity;
		}

		public IEnumerable< ThreeDCartOrderStatus > GetOrderStatuses()
		{
			const string sql = "select id, StatusID, StatusDefinition, StatusText, Visible from order_Status";
			var result = this._webRequestServices.Get< ThreeDCartOrderStatuses >( this._config,
				() => this._advancedService.runQuery( this._config.StoreUrl, this._config.UserKey, sql, "" ) );
			return result.Statuses;
		}

		public async Task< IEnumerable< ThreeDCartOrderStatus > > GetOrderStatusesAsync()
		{
			const string sql = "select id, StatusID, StatusDefinition, StatusText, Visible from order_Status";
			var result = await this._webRequestServices.GetAsync< ThreeDCartOrderStatuses >( this._config,
				async () => ( await this._advancedService.runQueryAsync( this._config.StoreUrl, this._config.UserKey, sql, "" ) ).Body.runQueryResult );
			return result.Statuses;
		}

		private IEnumerable< ThreeDCartOrder > FilterNotCompletedOrdersIfNeeded( IEnumerable< ThreeDCartOrder > orders, bool includeNotCompleted )
		{
			return includeNotCompleted ? orders : orders.Where( x => x.OrderStatus != ThreeDCartOrderStatusEnum.NotCompleted );
		}

		private List< ThreeDCartOrder > SetTimeZoneAndFilterByDate( IEnumerable< ThreeDCartOrder > orders, DateTime? startDateUtc, DateTime? endDateUtc )
		{
			if( startDateUtc == null )
				startDateUtc = DateTime.MinValue;
			if( endDateUtc == null )
				endDateUtc = DateTime.MaxValue;

			var result = new List< ThreeDCartOrder >();
			foreach( var order in orders )
			{
				order.TimeZone = this._config.TimeZone;
				if( order.DateTimeCreatedUtc >= startDateUtc && order.DateTimeCreatedUtc <= endDateUtc ||
				    order.DateTimeUpdatedUtc >= startDateUtc && order.DateTimeUpdatedUtc <= endDateUtc )
					result.Add( order );
			}
			return result;
		}

		private string GetDate( DateTime? date )
		{
			return date == null ? string.Empty : string.Format( _culture, "{0:MM/dd/yyyy}", date );
		}
	}
}