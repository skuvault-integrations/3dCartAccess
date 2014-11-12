using System.Collections.Generic;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	public class ThreeDCartShippingInformation
	{
		[ XmlElement( ElementName = "Shipment" ) ]
		public ThreeDCartShipment Shipment{ get; set; }

		[ XmlElement( ElementName = "OrderItems" ) ]
		public List< ThreeDCartOrderItem > OrderItem{ get; set; }
	}
}