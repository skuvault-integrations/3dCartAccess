using System.Threading.Tasks;
using ThreeDCartAccess.Models.Order;

namespace ThreeDCartAccess
{
	public interface IThreeDCartOrdersService
	{
		ThreeDCartOrder GetOrders();
		Task< ThreeDCartOrder > GetOrdersAsync();
	}
}