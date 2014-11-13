using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	public class ThreeDCartOrderItem
	{
		[ XmlElement( ElementName = "ShipmentID" ) ]
		public long ShipmentId{ get; set; }

		[ XmlElement( ElementName = "ProductID" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "ProductName" ) ]
		public string ProductName{ get; set; }

		[ XmlElement( ElementName = "Quantity" ) ]
		public int Quantity{ get; set; }

		[ XmlElement( ElementName = "UnitPrice" ) ]
		public decimal UnitPrice{ get; set; }

		[ XmlElement( ElementName = "UnitCost" ) ]
		public decimal UnitCost{ get; set; }

		[ XmlElement( ElementName = "OptionPrice" ) ]
		public decimal OptionPrice{ get; set; }

		[ XmlElement( ElementName = "Weight" ) ]
		public decimal Weight{ get; set; }

		[ XmlElement( ElementName = "WarehouseID" ) ]
		public long WarehouseId{ get; set; }

		[ XmlElement( ElementName = "DateAdded" ) ]
		public string DateAdded{ get; set; }

		[ XmlElement( ElementName = "PageAdded" ) ]
		public string PageAdded{ get; set; }
	}
}