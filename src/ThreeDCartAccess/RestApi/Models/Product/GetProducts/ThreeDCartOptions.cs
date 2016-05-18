namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartOptions
	{
		public int? OptionID{ get; set; }
		public string OptionName{ get; set; }
		public bool? OptionSelected{ get; set; }
		public bool? OptionHide{ get; set; }
		public double? OptionValue{ get; set; }
		public string OptionPartNumber{ get; set; }
		public int? OptionSorting{ get; set; }
		public string OptionImagePath{ get; set; }
		public int? OptionBundleCatalogId{ get; set; }
		public int? OptionBundleQuantity{ get; set; }
	}
}