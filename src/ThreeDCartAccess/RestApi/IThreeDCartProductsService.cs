using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.RestApi.Models.Product;

namespace ThreeDCartAccess.RestApi
{
	public interface IThreeDCartProductsService
	{
		List< ThreeDCartProduct > GetAllProducts();
		void GetAllProducts( Action< ThreeDCartProduct > processAction );

		Task< List< ThreeDCartProduct > > GetAllProductsAsync();
		Task GetAllProductsAsync( Action< ThreeDCartProduct > processAction );
	}
}