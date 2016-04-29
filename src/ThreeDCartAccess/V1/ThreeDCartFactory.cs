using ThreeDCartAccess.V1.Models.Configuration;

namespace ThreeDCartAccess.V1
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