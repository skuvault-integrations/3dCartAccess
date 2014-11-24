﻿using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "OrdersCountResponse" ) ]
	public class ThreeDCartOrdersCount
	{
		[ XmlElement( ElementName = "Quantity" ) ]
		public int Quantity{ get; set; }
	}
}