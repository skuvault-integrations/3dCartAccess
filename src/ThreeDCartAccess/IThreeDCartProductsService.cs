using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Models.Product;

namespace ThreeDCartAccess
{
	public interface IThreeDCartProductsService
	{
		bool IsGetProducts();
		IEnumerable< ThreeDCartProduct > GetProducts();
		Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync();

		bool IsGetInventory();
		IEnumerable< ThreeDCartInventory > GetInventory();
		Task< IEnumerable< ThreeDCartInventory > > GetInventoryAsync();

		IEnumerable< ThreeDCartUpdateInventory > UpdateInventory( IEnumerable< ThreeDCartUpdateInventory > inventory, bool updateProductTotalStock = false );
		Task< IEnumerable< ThreeDCartUpdateInventory > > UpdateInventoryAsync( IEnumerable< ThreeDCartUpdateInventory > inventory, bool updateProductTotalStock = false );
	}
}