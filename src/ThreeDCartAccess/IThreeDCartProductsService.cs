using System.Threading.Tasks;
using ThreeDCartAccess.Models.Product;

namespace ThreeDCartAccess
{
	public interface IThreeDCartProductsService
	{
		ThreeDCartProduct GetProducts();
		Task< ThreeDCartProduct > GetProductsAsync();
	}
}