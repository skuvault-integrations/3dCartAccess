using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	public class ThreeDCartShipment
	{
		[ XmlElement( ElementName = "ShipmentID" ) ]
		public string ShipmentId{ get; set; }

		[ XmlElement( ElementName = "ShipmentDate" ) ]
		public string ShipmentDate{ get; set; }

		[ XmlElement( ElementName = "Shipping" ) ]
		public string Shipping{ get; set; }

		[ XmlElement( ElementName = "Method" ) ]
		public string Method{ get; set; }

		[ XmlElement( ElementName = "FirstName" ) ]
		public string FirstName{ get; set; }

		[ XmlElement( ElementName = "LastName" ) ]
		public string LastName{ get; set; }

		[ XmlElement( ElementName = "Company" ) ]
		public string Company{ get; set; }

		[ XmlElement( ElementName = "Address" ) ]
		public string Address{ get; set; }

		[ XmlElement( ElementName = "Address2" ) ]
		public string Address2{ get; set; }

		[ XmlElement( ElementName = "City" ) ]
		public string City{ get; set; }

		[ XmlElement( ElementName = "ZipCode" ) ]
		public string ZipCode{ get; set; }

		[ XmlElement( ElementName = "StateCode" ) ]
		public string StateCode{ get; set; }

		[ XmlElement( ElementName = "CountryCode" ) ]
		public string CountryCode{ get; set; }

		[ XmlElement( ElementName = "Phone" ) ]
		public string Phone{ get; set; }

		[ XmlElement( ElementName = "Weight" ) ]
		public string Weight{ get; set; }

		[ XmlElement( ElementName = "Status" ) ]
		public string Status{ get; set; }

		[ XmlElement( ElementName = "InternalComment" ) ]
		public string InternalComment{ get; set; }

		[ XmlElement( ElementName = "TrackingCode" ) ]
		public string TrackingCode{ get; set; }
	}
}