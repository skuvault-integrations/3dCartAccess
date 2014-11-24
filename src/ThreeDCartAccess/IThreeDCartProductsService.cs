using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Models.Product;

namespace ThreeDCartAccess
{
	public interface IThreeDCartProductsService
	{
		IEnumerable< ThreeDCartProduct > GetProducts();
		Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync();

		int GetProductsCount();
		Task< int > GetProductsCountAsync();

		ThreeDCartUpdateInventory UpdateInventory( ThreeDCartUpdateInventory inventory );
		Task< ThreeDCartUpdateInventory > UpdateInventoryAsync( ThreeDCartUpdateInventory inventory );

		IEnumerable< ThreeDCartUpdateInventory > UpdateInventory( IEnumerable< ThreeDCartUpdateInventory > inventory );
		Task< IEnumerable< ThreeDCartUpdateInventory > > UpdateInventoryAsync( IEnumerable< ThreeDCartUpdateInventory > inventory );
	}
}