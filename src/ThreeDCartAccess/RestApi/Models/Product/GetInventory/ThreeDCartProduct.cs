using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeDCartAccess.RestApi.Models.Product.GetInventory
{
	public class ThreeDCartProduct
	{
		public ThreeDCartProductSKU SKUInfo{ get; set; }
		public List< ThreeDCartAdvancedOption > AdvancedOptionList{ get; set; }

		public int InventoryControl{ get; set; }

		[ IgnoreDataMember ]
		public ThreeDCartInventoryControlEnum InventoryControlEnum
		{
			get { return ( ThreeDCartInventoryControlEnum )this.InventoryControl; }
		}
	}

	public enum ThreeDCartInventoryControlEnum
	{
		Undefined = 0,
		Default = -1,
		OutOfStock = 1,
		BackOrder = 2,
		WaitingList = 3
	}
}