using System.Collections.Generic;
using System.Linq;
using ThreeDCartAccess.RestApi.Models.Product.GetProducts;

namespace ThreeDCartAccess.RestApi.Models.Product.UpdateInventory
{
	public class ThreeDCartProduct
	{
		public ThreeDCartProductSKU SKUInfo{ get; set; }
		//TODO: Can not support until bug not fixed in 3DCart
		//public List< ThreeDCartAdvancedOption > AdvancedOptionList{ get; set; }

		public ThreeDCartProduct()
		{
		}

		public ThreeDCartProduct( GetInventory.ThreeDCartProduct product )
		{
			this.SKUInfo = new ThreeDCartProductSKU( product.SKUInfo );
			//this.AdvancedOptionList = product.AdvancedOptionList.Select( x => x.Clone() ).ToList();
		}
	}
}