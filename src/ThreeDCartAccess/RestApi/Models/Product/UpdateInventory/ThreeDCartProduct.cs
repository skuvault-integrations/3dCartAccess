using System.Collections.Generic;
using System.Linq;
using ThreeDCartAccess.RestApi.Models.Product.GetProducts;

namespace ThreeDCartAccess.RestApi.Models.Product.UpdateInventory
{
	public class ThreeDCartProduct
	{
		public ThreeDCartProductSKU SKUInfo{ get; set; }
		public List< ThreeDCartAdvancedOption > AdvancedOptionList{ get; set; }

		public ThreeDCartProduct()
		{
		}

		public ThreeDCartProduct( GetInventory.ThreeDCartProduct product )
		{
			this.SKUInfo = new ThreeDCartProductSKU( product.SKUInfo );
			this.AdvancedOptionList = product.AdvancedOptionList.Select( x => x.Clone() ).ToList();
		}
	}

	internal class ThreeDCartProductWithoutOptions
	{
		public ThreeDCartProductSKU SKUInfo{ get; set; }

		public ThreeDCartProductWithoutOptions()
		{
		}

		public ThreeDCartProductWithoutOptions( ThreeDCartProduct product )
		{
			this.SKUInfo = product.SKUInfo;
		}
	}
}