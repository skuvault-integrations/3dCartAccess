using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Models.Order;

namespace ThreeDCartAccess
{
	public interface IThreeDCartOrdersService
	{
		IEnumerable< ThreeDCartOrder > GetOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false );
		Task< IEnumerable< ThreeDCartOrder > > GetOrdersAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false );

		IEnumerable< ThreeDCartOrderStatus > GetOrderStatuses();
		Task< IEnumerable< ThreeDCartOrderStatus > > GetOrderStatusesAsync();

		int GetOrdersCount( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
		Task< int > GetOrdersCountAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
	}
}