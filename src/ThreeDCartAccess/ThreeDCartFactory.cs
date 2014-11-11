using ThreeDCartAccess.Models.Configuration;

namespace ThreeDCartAccess
{
	public interface IThreeDCartFactory
	{
		IThreeDCartProductsService CreateProductsService( ThreeDCartConfig config );
	}

	public class ThreeDCartFactory: IThreeDCartFactory
	{
		public IThreeDCartProductsService CreateProductsService( ThreeDCartConfig config )
		{
			return new ThreeDCartProductsService( config );
		}
	}
}