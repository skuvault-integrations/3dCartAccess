using System.Threading.Tasks;
using System.Xml.Linq;
using CuttingEdge.Conditions;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.Models.Configuration;
using ThreeDCartAccess.Models.Product;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess
{
	public class ThreeDCartProductsService: IThreeDCartProductsService
	{
		public readonly ThreeDCartConfig _config;
		private readonly cartAPISoapClient _service;
		private readonly WebRequestServices _webRequestServices;

		public ThreeDCartProductsService( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public ThreeDCartProduct GetProducts()
		{
			var result = this._webRequestServices.Get< ThreeDCartProduct, XElement >(
				() => this._service.getProduct( this._config.StoreUrl, this._config.UserKey, 100, 1, "", "" ) );
			return result;
		}

		public async Task< ThreeDCartProduct > GetProductsAsync()
		{
			var result = await this._webRequestServices.GetAsync< ThreeDCartProduct, XElement >(
				async () =>
				{
					var funcRes = await this._service.getProductAsync( this._config.StoreUrl, this._config.UserKey, 100, 1, "", "" );
					return funcRes.Body.getProductResult;
				} );

			return result;
		}
	}
}