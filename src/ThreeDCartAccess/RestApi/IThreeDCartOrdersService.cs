using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.RestApi.Models.Order;

namespace ThreeDCartAccess.RestApi
{
	public interface IThreeDCartOrdersService
	{
		List< ThreeDCartOrder > GetAllOrders();
		Task< List< ThreeDCartOrder > > GetAllOrdersAsync();

		List< ThreeDCartOrder > GetNewOrders( DateTime startDateTime, DateTime endDateTime );
		void GetNewOrders( DateTime startDateTime, DateTime endDateTime, Action< ThreeDCartOrder > processAction );

		Task< List< ThreeDCartOrder > > GetNewOrdersAsync( DateTime startDateTime, DateTime endDateTime );
		Task GetNewOrdersAsync( DateTime startDateTime, DateTime endDateTime, Action< ThreeDCartOrder > processAction );
	}
}