namespace ThreeDCartAccess.RestApi.Models.Product.UpdateInventory
{
	public class ThreeDCartProductSKU
	{
		public long CatalogID{ get; set; }
		public decimal Stock{ get; set; }

		public ThreeDCartProductSKU()
		{
		}

		public ThreeDCartProductSKU( GetInventory.ThreeDCartProductSKU productSku )
		{
			this.CatalogID = productSku.CatalogID;
			this.Stock = productSku.Stock;
		}
	}
}