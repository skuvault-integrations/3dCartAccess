using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.RestApi.Models.Order;

namespace ThreeDCartAccess.RestApi
{
	public interface IThreeDCartOrdersService
	{
		List< ThreeDCartOrder > GetAllOrders();
		void GetAllOrders( Action< ThreeDCartOrder > processAction );

		Task< List< ThreeDCartOrder > > GetAllOrdersAsync();
		Task GetAllOrdersAsync( Action< ThreeDCartOrder > processAction );
	}
}