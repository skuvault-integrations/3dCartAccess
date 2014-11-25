using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
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

		public ThreeDCartOrdersService( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
			this._advancedService = new cartAPIAdvancedSoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public IEnumerable< ThreeDCartOrder > GetOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = new List< ThreeDCartOrder >();
			var ordersCount = this.GetOrdersCount( startDate, endDate );
			for( var i = 1; i <= ordersCount; i += _batchSize )
			{
				var portion = this._webRequestServices.Get< ThreeDCartOrders >( this._config,
					() => this._service.getOrder( this._config.StoreUrl, this._config.UserKey, _batchSize, i, true, "", "", startDate, endDate, "" ) );
				var filtered = this.FilterNotCompletedOrdersIfNeeded( portion.Orders, includeNotCompleted );
				result.AddRange( filtered );
			}

			result = this.SetTimeZone( result );
			result = this.FilterByDateIfNeeded( result, startDateUtc, endDateUtc );
			return result;
		}

		public async Task< IEnumerable< ThreeDCartOrder > > GetOrdersAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false )
		{
			var startDate = this.GetDate( startDateUtc );
			var endDate = this.GetDate( endDateUtc );
			var result = new List< ThreeDCartOrder >();
			var ordersCount = await this.GetOrdersCountAsync( startDate, endDate );
			for( var i = 1; i <= ordersCount; i += _batchSize )
			{
				var portion = await this._webRequestServices.GetAsync< ThreeDCartOrders >( this._config,
					async () => ( await this._service.getOrderAsync( this._config.StoreUrl, this._config.UserKey, _batchSize, i, true, "", "", startDate, endDate, "" ) ).Body.getOrderResult );
				var filtered = this.FilterNotCompletedOrdersIfNeeded( portion.Orders, includeNotCompleted );
				result.AddRange( filtered );
			}

			result = this.SetTimeZone( result );
			result = this.FilterByDateIfNeeded( result, startDateUtc, endDateUtc );
			return result;
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

		private List< ThreeDCartOrder > SetTimeZone( List< ThreeDCartOrder > orders )
		{
			foreach( var order in orders )
			{
				order.TimeZone = this._config.TimeZone;
			}
			return orders;
		}

		private List< ThreeDCartOrder > FilterByDateIfNeeded( List< ThreeDCartOrder > orders, DateTime? startDateUtc, DateTime? endDateUtc )
		{
			if( startDateUtc == null && endDateUtc == null )
				return orders;
			return orders.Where( x => x.DateTimeCreatedUtc >= startDateUtc && x.DateTimeCreatedUtc <= endDateUtc ||
			                          x.DateTimeUpdatedUtc >= startDateUtc && x.DateTimeUpdatedUtc <= endDateUtc ).ToList();
		}

		private string GetDate( DateTime? date )
		{
			return date == null ? string.Empty : string.Format( _culture, "{0:MM/dd/yyyy}", date );
		}
	}
}