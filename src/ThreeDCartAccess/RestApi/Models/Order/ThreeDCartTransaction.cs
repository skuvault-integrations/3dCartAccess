using System;

namespace ThreeDCartAccess.RestApi.Models.Order
{
	public class ThreeDCartTransaction
	{
		public int? TransactionIndexID{ get; set; }
		public int? OrderID{ get; set; }
		public string TransactionID{ get; set; }
		public DateTime TransactionDateTime{ get; set; }
		public string TransactionType{ get; set; }
		public string TransactionMethod{ get; set; }
		public decimal? TransactionAmount{ get; set; }
		public string TransactionApproval{ get; set; }
		public string TransactionReference{ get; set; }
		public int? TransactionGatewayID{ get; set; }
		public string TransactionCVV2{ get; set; }
		public string TransactionAVS{ get; set; }
		public string TransactionResponseText{ get; set; }
		public string TransactionResponseCode{ get; set; }
		public int? TransactionCaptured{ get; set; }
	}
}