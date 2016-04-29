using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ThreeDCartAccess.V1.Models.Product
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "runQueryResponse" ) ]
	public class ThreeDCartInventories: RunQueryResponse
	{
		[ XmlElement( ElementName = "runQueryRecord" ) ]
		public List< ThreeDCartInventory > Inventory{ get; set; }

		[ XmlIgnore ]
		public bool IsFullInventory{ get; set; }
	}
}