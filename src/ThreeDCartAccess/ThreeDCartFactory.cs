using ThreeDCartAccess.Models.Configuration;

namespace ThreeDCartAccess
{
	public interface IThreeDCartFactory
	{
		IThreeDCartProductsService CreateProductsService( ThreeDCartConfig config, bool retryOnlyOneTime = false );
		IThreeDCartOrdersService CreateOrdersService( ThreeDCartConfig config, bool retryOnlyOneTime = false );
	}

	public class ThreeDCartFactory: IThreeDCartFactory
	{
		public IThreeDCartProductsService CreateProductsService( ThreeDCartConfig config, bool retryOnlyOneTime = false )
		{
			return new ThreeDCartProductsService( config, retryOnlyOneTime );
		}

		public IThreeDCartOrdersService CreateOrdersService( ThreeDCartConfig config, bool retryOnlyOneTime = false )
		{
			return new ThreeDCartOrdersService( config, retryOnlyOneTime );
		}
	}
}