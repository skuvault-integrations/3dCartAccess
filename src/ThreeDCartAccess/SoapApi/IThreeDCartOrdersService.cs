using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.SoapApi.Models.Order;

namespace ThreeDCartAccess.SoapApi
{
	public interface IThreeDCartOrdersService
	{
		bool IsGetNewOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
		List< ThreeDCartOrder > GetNewOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< List< ThreeDCartOrder > > GetNewOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		List< ThreeDCartOrder > GetOrdersByNumber( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc );
		Task< List< ThreeDCartOrder > > GetOrdersByNumberAsync( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc );

		ThreeDCartOrder GetOrderByNumber( string invoiceNumber );
		Task< ThreeDCartOrder > GetOrderByNumberAsync( string invoiceNumber );

		List< ThreeDCartOrderStatus > GetOrderStatuses();
		Task< List< ThreeDCartOrderStatus > > GetOrderStatusesAsync();

		int GetOrdersCount( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
		Task< int > GetOrdersCountAsync( DateTime? startDateUtc = null, DateTime? endDateUtc = null );
	}
}