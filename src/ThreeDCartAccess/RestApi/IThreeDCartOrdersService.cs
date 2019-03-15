using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.RestApi.Models.Order;

namespace ThreeDCartAccess.RestApi
{
	public interface IThreeDCartOrdersService
	{
		bool IsGetNewOrders( DateTime? startDateUtc = null, DateTime? endDateUtc = null );

		List< ThreeDCartOrder > GetNewOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< List< ThreeDCartOrder > > GetNewOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		void GetNewOrders( DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction );
		Task GetNewOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction );
		Task< List< ThreeDCartOrder > > GetUpdatedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		List< ThreeDCartOrder > GetOrdersByNumber( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc );
		Task< List< ThreeDCartOrder > > GetOrdersByNumberAsync( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc );

		void GetOrdersByNumber( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction );
		Task GetOrdersByNumberAsync( List< string > invoiceNumbers, DateTime startDateUtc, DateTime endDateUtc, Action< ThreeDCartOrder > processAction );
	}
}