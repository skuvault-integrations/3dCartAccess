namespace ThreeDCartAccess.RestApi.Models.Product.GetInventory
{
	public class ThreeDCartProductSKU
	{
		public long CatalogID{ get; set; }
		public string SKU{ get; set; }
		public decimal Stock{ get; set; }
	}
}