namespace ThreeDCartAccess.RestApi.Models.Product
{
	public class ThreeDCartProductForUpdatingInventory
	{
		public ThreeDCartProductSKUForUpdatingInventory SKUInfo{ get; set; }
		//TODO: Can not support until bug not fixed in 3DCart
		//public List< ThreeDCartAdvancedOption > AdvancedOptionList{ get; set; }

		public ThreeDCartProductForUpdatingInventory()
		{
		}

		public ThreeDCartProductForUpdatingInventory( ThreeDCartProduct product )
		{
			this.SKUInfo = new ThreeDCartProductSKUForUpdatingInventory( product.SKUInfo );
			//this.AdvancedOptionList = product.AdvancedOptionList;
		}
	}

	public class ThreeDCartProductSKUForUpdatingInventory
	{
		public long CatalogID{ get; set; }
		public string SKU{ get; set; }
		public double Stock{ get; set; }

		public ThreeDCartProductSKUForUpdatingInventory()
		{
		}

		public ThreeDCartProductSKUForUpdatingInventory( ThreeDCartProductSKU productSku )
		{
			this.CatalogID = productSku.CatalogID;
			this.SKU = productSku.SKU;
			this.Stock = productSku.Stock.GetValueOrDefault();
		}
	}
}