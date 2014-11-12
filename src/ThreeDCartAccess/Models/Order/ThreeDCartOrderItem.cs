using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	public class ThreeDCartOrderItem
	{
		[ XmlElement( ElementName = "ShipmentID" ) ]
		public string ShipmentID{ get; set; }

		[ XmlElement( ElementName = "ProductID" ) ]
		public string ProductID{ get; set; }

		[ XmlElement( ElementName = "ProductName" ) ]
		public string ProductName{ get; set; }

		[ XmlElement( ElementName = "Quantity" ) ]
		public string Quantity{ get; set; }

		[ XmlElement( ElementName = "UnitPrice" ) ]
		public string UnitPrice{ get; set; }

		[ XmlElement( ElementName = "UnitCost" ) ]
		public string UnitCost{ get; set; }

		[ XmlElement( ElementName = "OptionPrice" ) ]
		public string OptionPrice{ get; set; }

		[ XmlElement( ElementName = "Weight" ) ]
		public string Weight{ get; set; }

		[ XmlElement( ElementName = "WarehouseID" ) ]
		public string WarehouseID{ get; set; }

		[ XmlElement( ElementName = "DateAdded" ) ]
		public string DateAdded{ get; set; }

		[ XmlElement( ElementName = "PageAdded" ) ]
		public string PageAdded{ get; set; }
	}
}