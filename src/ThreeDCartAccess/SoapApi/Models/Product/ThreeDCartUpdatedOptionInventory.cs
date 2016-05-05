using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.SoapApi.Models.Product
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "runQueryResponse" ) ]
	public class ThreeDCartUpdatedOptionInventory
	{
		[ XmlElement( ElementName = "queryResult1" ) ]
		public string Result1{ get; set; }

		[ XmlElement( ElementName = "queryResult2" ) ]
		public string Result2{ get; set; }

		[ XmlElement( ElementName = "queryResult3" ) ]
		public string Result3{ get; set; }
	}
}