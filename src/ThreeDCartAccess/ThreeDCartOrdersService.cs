using System.Threading.Tasks;
using CuttingEdge.Conditions;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.Models.Configuration;
using ThreeDCartAccess.Models.Order;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess
{
	public class ThreeDCartOrdersService: IThreeDCartOrdersService
	{
		public readonly ThreeDCartConfig _config;
		private readonly cartAPISoapClient _service;
		private readonly WebRequestServices _webRequestServices;

		public ThreeDCartOrdersService( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public ThreeDCartOrder GetOrders()
		{
			var result = this._webRequestServices.Get< ThreeDCartOrder >( this._config,
				() => this._service.getOrder( this._config.StoreUrl, this._config.UserKey, 100, 1, false, "", "", "", "", "" ) );
			return result;
		}

		public async Task< ThreeDCartOrder > GetOrdersAsync()
		{
			var result = await this._webRequestServices.GetAsync< ThreeDCartOrder >( this._config,
				async () => ( await this._service.getOrderAsync( this._config.StoreUrl, this._config.UserKey, 100, 1, false, "", "", "", "", "" ) ).Body.getOrderResult );
			return result;
		}
	}
}