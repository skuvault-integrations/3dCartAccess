using System.Collections.Generic;
using System.Xml.Serialization;

namespace ThreeDCartAccess.V1.Models.Order
{
	public class ThreeDCartShippingInformation
	{
		[ XmlElement( ElementName = "Shipment" ) ]
		public ThreeDCartShipment Shipment{ get; set; }

		[ XmlArray( ElementName = "OrderItems" ) ]
		[ XmlArrayItem( ElementName = "Item" ) ]
		public List< ThreeDCartOrderItem > OrderItems{ get; set; }
	}
}