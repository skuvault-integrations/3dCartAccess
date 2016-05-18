namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartProductSKU
	{
		public long CatalogID{ get; set; }
		public string SKU{ get; set; }
		public string Name{ get; set; }
		public decimal? Cost{ get; set; }
		public decimal? Price{ get; set; }
		public decimal? RetailPrice{ get; set; }
		public decimal? SalePrice{ get; set; }
		public bool? OnSale{ get; set; }
		public decimal? Stock{ get; set; }
	}
}