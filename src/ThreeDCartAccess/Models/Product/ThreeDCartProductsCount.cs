using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Product
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "GetProductCountResponse" ) ]
	public class ThreeDCartProductsCount
	{
		[ XmlElement( ElementName = "ProductQuantity" ) ]
		public int Quantity{ get; set; }
	}
}