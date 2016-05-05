namespace ThreeDCartAccess.RestApi.Models
{
	public class ThreeDCartError
	{
		public string Key{ get; set; }
		public string Value{ get; set; }
		public int Status{ get; set; }
		public string Message{ get; set; }
	}
}