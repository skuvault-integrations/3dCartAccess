using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.V1.Models.Product
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "UpdateInventoryResponse" ) ]
	public class ThreeDCartUpdateInventory
	{
		[ XmlElement( ElementName = "ProductID" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "AO_Sufix" ) ]
		public string OptionCode{ get; set; }

		[ XmlElement( ElementName = "NewInventory" ) ]
		public int NewQuantity{ get; set; }
	}
}