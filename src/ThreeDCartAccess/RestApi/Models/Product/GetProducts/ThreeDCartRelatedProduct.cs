namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartRelatedProduct
	{
		public int? RelatedIndexID{ get; set; }
		public int RelatedProductID{ get; set; }
		public int RelatedProductSorting{ get; set; }
	}
}