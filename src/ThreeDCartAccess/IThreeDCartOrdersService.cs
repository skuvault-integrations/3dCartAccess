using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Models.Order;

namespace ThreeDCartAccess
{
	public interface IThreeDCartOrdersService
	{
		IEnumerable< ThreeDCartOrder > GetOrders();
		Task< IEnumerable< ThreeDCartOrder > > GetOrdersAsync();

		IEnumerable< ThreeDCartOrder > GetOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< IEnumerable< ThreeDCartOrder > > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		IEnumerable< ThreeDCartOrderStatus > GetOrderStatuses();
		Task< IEnumerable< ThreeDCartOrderStatus > > GetOrderStatusesAsync();
	}
}