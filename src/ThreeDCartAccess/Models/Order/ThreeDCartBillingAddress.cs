using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	public class ThreeDCartBillingAddress
	{
		[ XmlElement( ElementName = "FirstName" ) ]
		public string FirstName{ get; set; }

		[ XmlElement( ElementName = "LastName" ) ]
		public string LastName{ get; set; }

		[ XmlElement( ElementName = "Email" ) ]
		public string Email{ get; set; }

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

		[ XmlElement( ElementName = "Company" ) ]
		public string Company{ get; set; }
	}
}