using CuttingEdge.Conditions;
using ServiceStack;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.Models;
using ThreeDCartAccess.Models.Configuration;
using ThreeDCartAccess.Models.Product;
using ThreeDCartAccess.ThreeDCartService;

namespace ThreeDCartAccess
{
	public class ThreeDCartProductsService: IThreeDCartProductsService
	{
		public readonly ThreeDCartConfig _config;
		private readonly cartAPISoapClient _service;

		public ThreeDCartProductsService( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._service = new cartAPISoapClient();
		}

		public void GetProducts()
		{
			var res = this._service.getProduct( _config.StoreUrl, _config.UserKey, 100, 1, "", "" );

			if( res.Name.LocalName == "Error" )
			{ 
				var result = XmlSerializeHelpers.Deserialize< ThreeDCartError >( res.ToString() );
			}
			else
			{
				var result = XmlSerializeHelpers.Deserialize< ThreeDCartProduct >( res.ToString() );
			}
		}
	}
}