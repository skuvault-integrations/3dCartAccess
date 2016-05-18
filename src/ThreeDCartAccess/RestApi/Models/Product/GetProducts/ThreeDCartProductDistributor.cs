namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartProductDistributor
	{
		public int DistributorID{ get; set; }

		public string DistributorName{ get; set; }

		public double DistributorItemCost{ get; set; }

		public string DistributorItemID{ get; set; }

		public string DistributorStockID{ get; set; }
	}
}