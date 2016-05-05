using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccess.RestApi
{
	public interface IThreeDCartFactory
	{
		IThreeDCartProductsService CreateProductsService( ThreeDCartConfig config );
		IThreeDCartOrdersService CreateOrdersService( ThreeDCartConfig config );
	}

	public class ThreeDCartFactory: IThreeDCartFactory
	{
		public IThreeDCartProductsService CreateProductsService( ThreeDCartConfig config )
		{
			return new ThreeDCartProductsService( config );
		}

		public IThreeDCartOrdersService CreateOrdersService( ThreeDCartConfig config )
		{
			return new ThreeDCartOrdersService( config );
		}
	}
}