using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "runQueryResponse" ) ]
	public class ThreeDCartOrderStatuses
	{
		[ XmlElement( ElementName = "runQueryRecord" ) ]
		public List< ThreeDCartOrderStatus > Statuses{ get; set; }
	}
}