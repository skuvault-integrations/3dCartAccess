using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.RestApi.Models.Product.GetProducts;

namespace ThreeDCartAccess.RestApi
{
	public interface IThreeDCartProductsService
	{
		bool IsGetProducts();

		List< ThreeDCartProduct > GetProducts();
		void GetProducts( Action< ThreeDCartProduct > processAction );

		Task< List< ThreeDCartProduct > > GetProductsAsync();
		Task GetProductsAsync( Action< ThreeDCartProduct > processAction );

		List< Models.Product.GetInventory.ThreeDCartProduct > GetInventory();
		Task< List< Models.Product.GetInventory.ThreeDCartProduct > > GetInventoryAsync();

		void UpdateInventory( Models.Product.UpdateInventory.ThreeDCartProduct inventory );
		Task UpdateInventoryAsync( Models.Product.UpdateInventory.ThreeDCartProduct inventory );

		void UpdateInventory( List< Models.Product.UpdateInventory.ThreeDCartProduct > inventory );
		Task UpdateInventoryAsync( List< Models.Product.UpdateInventory.ThreeDCartProduct > inventory );
	}
}