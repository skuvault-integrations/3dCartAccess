namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartAdvancedOption
	{
		public string AdvancedOptionCode{ get; set; }
		public string AdvancedOptionSufix{ get; set; }
		public string AdvancedOptionName{ get; set; }
		public decimal AdvancedOptionCost{ get; set; }
		public int AdvancedOptionStock{ get; set; }
		public decimal AdvancedOptionWeight{ get; set; }
		public decimal AdvancedOptionPrice{ get; set; }
		public string AdvancedOptionGTIN{ get; set; }

		public ThreeDCartAdvancedOption Clone()
		{
			return ( ThreeDCartAdvancedOption )this.MemberwiseClone();
		}
	}
}