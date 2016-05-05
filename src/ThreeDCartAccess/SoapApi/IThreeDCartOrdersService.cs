using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.SoapApi.Models.Order;

namespace ThreeDCartAccess.SoapApi
{
	public interface IThreeDCartOrdersService
	{
		bool IsGetNewOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
		IEnumerable< ThreeDCartOrder > GetNewOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false );
		Task< IEnumerable< ThreeDCartOrder > > GetNewOrdersAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false );

		IEnumerable< ThreeDCartOrder > GetOrders( IEnumerable< string > invoiceNumbers, DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false );
		Task< IEnumerable< ThreeDCartOrder > > GetOrdersAsync( IEnumerable< string > invoiceNumbers, DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool includeNotCompleted = false );

		ThreeDCartOrder GetOrder( string invoiceNumber );
		Task< ThreeDCartOrder > GetOrderAsync( string invoiceNumber );

		IEnumerable< ThreeDCartOrderStatus > GetOrderStatuses();
		Task< IEnumerable< ThreeDCartOrderStatus > > GetOrderStatusesAsync();

		int GetOrdersCount( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
		Task< int > GetOrdersCountAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
	}
}