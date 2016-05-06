using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

		#region Get All Orders
		public List< ThreeDCartOrder > GetAllOrders()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartOrder >();
			this.GetCollection< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetOrdersEnpoint( offset, BatchSize ), portion =>
			{
				this.SetTimeZone( portion );
				result.AddRange( portion );
			} );
			return result;
		}

		public async Task< List< ThreeDCartOrder > > GetAllOrdersAsync()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartOrder >();
			await this.GetCollectionAsync< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetOrdersEnpoint( offset, BatchSize ), portion =>
			{
				this.SetTimeZone( portion );
				result.AddRange( portion );
			} );
			return result;
		}
		#endregion

		#region Get New Orders
		public List< ThreeDCartOrder > GetNewOrders( DateTime startDateTime, DateTime endDateTime )
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartOrder >();
			this.GetCollection< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetNewOrdersEnpoint( offset, BatchSize, startDateTime, endDateTime, this.Config.TimeZone ), portion =>
			{
				portion = this.SetTimeZoneAndFilter( portion, startDateTime, endDateTime );
				result.AddRange( portion );
			} );
			return result;
		}

		public void GetNewOrders( DateTime startDateTime, DateTime endDateTime, Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			this.GetCollection< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetNewOrdersEnpoint( offset, BatchSize, startDateTime, endDateTime, this.Config.TimeZone ), portion =>
			{
				portion = this.SetTimeZoneAndFilter( portion, startDateTime, endDateTime );
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}

		public async Task< List< ThreeDCartOrder > > GetNewOrdersAsync( DateTime startDateTime, DateTime endDateTime )
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartOrder >();
			await this.GetCollectionAsync< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetNewOrdersEnpoint( offset, BatchSize, startDateTime, endDateTime, this.Config.TimeZone ), portion =>
			{
				portion = this.SetTimeZoneAndFilter( portion, startDateTime, endDateTime );
				result.AddRange( portion );
			} );
			return result;
		}

		public async Task GetNewOrdersAsync( DateTime startDateTime, DateTime endDateTime, Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			await this.GetCollectionAsync< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetNewOrdersEnpoint( offset, BatchSize, startDateTime, endDateTime, this.Config.TimeZone ), portion =>
			{
				portion = this.SetTimeZoneAndFilter( portion, startDateTime, endDateTime );
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}
		#endregion

		private void SetTimeZone( IEnumerable< ThreeDCartOrder > orders )
		{
			foreach( var order in orders )
			{
				order.TimeZone = this.Config.TimeZone;
			}
		}

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
	}
}