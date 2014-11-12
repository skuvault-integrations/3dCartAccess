using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Product
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "UpdateInventoryResponse" ) ]
	public class ThreeDCartUpdatedInventory
	{
		[ XmlElement( ElementName = "ProductID" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "NewInventory" ) ]
		public int Quantity{ get; set; }
	}
}