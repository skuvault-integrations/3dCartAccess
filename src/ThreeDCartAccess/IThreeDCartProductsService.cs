using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.Models.Product;

namespace ThreeDCartAccess
{
	public interface IThreeDCartProductsService
	{
		IEnumerable< ThreeDCartProduct > GetProducts();
		Task< IEnumerable< ThreeDCartProduct > > GetProductsAsync();

		IEnumerable< ThreeDCartInventory > GetInventory();
		Task< IEnumerable< ThreeDCartInventory > > GetInventoryAsync();

		IEnumerable< ThreeDCartUpdateInventory > UpdateInventory( IEnumerable< ThreeDCartUpdateInventory > inventory, bool updateProductTotalStock = false );
		Task< IEnumerable< ThreeDCartUpdateInventory > > UpdateInventoryAsync( IEnumerable< ThreeDCartUpdateInventory > inventory, bool updateProductTotalStock = false );
	}
}