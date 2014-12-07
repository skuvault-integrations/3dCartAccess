using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Product
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "runQueryResponse" ) ]
	public class ThreeDCartInventories
	{
		[ XmlElement( ElementName = "runQueryRecord" ) ]
		public List< ThreeDCartInventory > Inventory{ get; set; }
	}
}