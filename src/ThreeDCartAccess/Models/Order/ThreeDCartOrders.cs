using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "Error" ) ]
	public class ThreeDCartOrders
	{
		[ XmlElement( ElementName = "Order" ) ]
		public List< ThreeDCartOrder > Orders{ get; set; }
	}
}