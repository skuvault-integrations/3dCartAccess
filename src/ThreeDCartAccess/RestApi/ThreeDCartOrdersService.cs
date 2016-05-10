using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Netco.Extensions;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.RestApi.Models.Order;

namespace ThreeDCartAccess.RestApi
{
	public class ThreeDCartOrdersService: ThreeDCartServiceBase, IThreeDCartOrdersService
	{
		public ThreeDCartOrdersService( ThreeDCartConfig config ): base( config )
		{
		}

		public bool IsGetNewOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null )
		{
			try
			{
				var marker = this.GetMarker();
				startDateUtc = startDateUtc ?? DateTime.UtcNow.AddDays( -30 );
				endDateUtc = endDateUtc ?? DateTime.UtcNow.AddDays( 1 );
				var endpoint = EndpointsBuilder.GetNewOrdersEnpoint( 1, BatchSize, startDateUtc.Value, endDateUtc.Value, this.Config.TimeZone );
				this.WebRequestServices.GetResponse< List< ThreeDCartOrder > >( endpoint, marker );
				return true;
			}
			catch( Exception )
			{
				return false;
			}
		}

		#region Get New Orders
		public List< ThreeDCartOrder > GetNewOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var result = new List< ThreeDCartOrder >();
			this.GetNewOrders( startDateUtc, endDateUtc, order => result.Add( order ) );
			return result;
		}

		public void GetNewOrders( DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			this.GetCollection< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetNewOrdersEnpoint( offset, BatchSize, startDateUtc, endDateUtc, this.Config.TimeZone ), portion =>
			{
				portion = this.SetTimeZoneAndFilter( portion, startDateUtc, endDateUtc );
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}

		public async Task< List< ThreeDCartOrder > > GetNewOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var result = new List< ThreeDCartOrder >();
			await this.GetNewOrdersAsync( startDateUtc, endDateUtc, order => result.Add( order ) );
			return result;
		}

		public async Task GetNewOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			await this.GetCollectionAsync< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetNewOrdersEnpoint( offset, BatchSize, startDateUtc, endDateUtc, this.Config.TimeZone ), portion =>
			{
				portion = this.SetTimeZoneAndFilter( portion, startDateUtc, endDateUtc );
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}
		#endregion

		#region Get Orders By Number
		public List< ThreeDCartOrder > GetOrdersByNumber( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc )
		{
			var result = new List< ThreeDCartOrder >();
			this.GetOrdersByNumber( invoiceNumbers, startDateUtc, endDateUtc, order => result.Add( order ) );
			return result;
		}

		public void GetOrdersByNumber( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			foreach( var invoiceNumber in invoiceNumbers )
			{
				var endpoint = EndpointsBuilder.GetOrderEnpoint( invoiceNumber );
				var portion = ActionPolicies.Get.Get( () => this.WebRequestServices.GetResponse< List< ThreeDCartOrder > >( endpoint, marker ) );
				if( portion == null )
					continue;

				portion = this.SetTimeZoneAndFilter( portion, startDateUtc, endDateUtc );
				foreach( var threeDCartOrder in portion )
				{
					processAction( threeDCartOrder );
				}
			}
		}

		public async Task< List< ThreeDCartOrder > > GetOrdersByNumberAsync( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc )
		{
			var result = new ConcurrentBag< ThreeDCartOrder >();
			await this.GetOrdersByNumberAsync( invoiceNumbers, startDateUtc, endDateUtc, order => result.Add( order ) );
			return result.ToList();
		}

		public async Task GetOrdersByNumberAsync( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			await invoiceNumbers.DoInBatchAsync( 10, async invoiceNumber =>
			{
				var endpoint = EndpointsBuilder.GetOrderEnpoint( invoiceNumber );
				var portion = await ActionPolicies.GetAsync.Get( async () => await this.WebRequestServices.GetResponseAsync< List< ThreeDCartOrder > >( endpoint, marker ) );
				if( portion == null )
					return;

				portion = this.SetTimeZoneAndFilter( portion, startDateUtc, endDateUtc );
				foreach( var threeDCartOrder in portion )
				{
					processAction( threeDCartOrder );
				}
			} );
		}
		#endregion

		#region Misc
		private List< ThreeDCartOrder > SetTimeZoneAndFilter( IEnumerable< ThreeDCartOrder > orders, DateTime startDateUtc, DateTime endDateUtc )
		{
			var result = new List< ThreeDCartOrder >();
			foreach( var order in orders )
			{
				if( order.OrderStatus == ThreeDCartOrderStatusEnum.NotCompleted )
					continue;

				order.TimeZone = this.Config.TimeZone;
				if( order.OrderDateUtc >= startDateUtc && order.OrderDateUtc <= endDateUtc ||
				    order.LastUpdateUtc >= startDateUtc && order.LastUpdateUtc <= endDateUtc )
					result.Add( order );
			}
			return result;
		}
		#endregion
	}
}