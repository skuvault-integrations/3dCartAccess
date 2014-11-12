using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Models.Product;

namespace ThreeDCartAccess
{
	public interface IThreeDCartProductsService
	{
		IEnumerable< ThreeDCartProduct > GetProducts();
		Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync();
	}
}