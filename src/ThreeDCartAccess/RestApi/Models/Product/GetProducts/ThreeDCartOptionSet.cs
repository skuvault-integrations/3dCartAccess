using System.Collections.Generic;

namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartOptionSet
	{
		public int? OptionSetID{ get; set; }
		public string OptionSetName{ get; set; }
		public int? OptionSorting{ get; set; }
		public bool? OptionRequired{ get; set; }
		public string OptionType{ get; set; }
		public string OptionURL{ get; set; }
		public string OptionAdditionalInformation{ get; set; }
		public int? OptionSizeLimit{ get; set; }
		public List< ThreeDCartOptions > OptionList{ get; set; }
	}
}