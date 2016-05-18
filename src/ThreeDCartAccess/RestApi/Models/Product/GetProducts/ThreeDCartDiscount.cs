namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartDiscount
	{
		public int? DiscountID{ get; set; }
		public int DiscountPriceLevel{ get; set; }
		public int DiscountLowbound{ get; set; }
		public int DiscountUpbound{ get; set; }
		public double DiscountPrice{ get; set; }
		public bool DiscountPercentage{ get; set; }
	}
}