using System;

namespace ThreeDCartAccess.RestApi.Models.Order
{
	public class ThreeDCartShipment
	{
		public int? ShipmentID{ get; set; }
		public DateTime? ShipmentLastUpdate{ get; set; }
		public int? ShipmentBoxes{ get; set; }
		public string ShipmentInternalComment{ get; set; }
		public int? ShipmentOrderStatus{ get; set; }
		public string ShipmentAddress{ get; set; }
		public string ShipmentAddress2{ get; set; }
		public string ShipmentAlias{ get; set; }
		public string ShipmentCity{ get; set; }
		public string ShipmentCompany{ get; set; }
		public decimal? ShipmentCost{ get; set; }
		public string ShipmentCountry{ get; set; }
		public string ShipmentEmail{ get; set; }
		public string ShipmentFirstName{ get; set; }
		public string ShipmentLastName{ get; set; }
		public int? ShipmentMethodID{ get; set; }
		public string ShipmentMethodName{ get; set; }
		public string ShipmentShippedDate{ get; set; }
		public string ShipmentPhone{ get; set; }
		public string ShipmentState{ get; set; }
		public string ShipmentZipCode{ get; set; }
		public decimal? ShipmentTax{ get; set; }
		public decimal? ShipmentWeight{ get; set; }
		public string ShipmentTrackingCode{ get; set; }
		public string ShipmentUserID{ get; set; }
		public int? ShipmentNumber{ get; set; }
		public int? ShipmentAddressTypeID{ get; set; }
	}
}