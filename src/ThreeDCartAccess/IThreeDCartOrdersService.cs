using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Models.Order;

namespace ThreeDCartAccess
{
	public interface IThreeDCartOrdersService
	{
		IEnumerable< ThreeDCartOrder > GetOrders();
		Task< IEnumerable< ThreeDCartOrder > > GetOrdersAsync();
	}
}