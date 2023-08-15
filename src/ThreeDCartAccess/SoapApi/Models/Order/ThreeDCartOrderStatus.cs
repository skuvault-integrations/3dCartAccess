using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.SoapApi.Models.Order
{
	public class ThreeDCartOrderStatus
	{
		[ XmlElement( ElementName = "StatusID" ) ]
		public int StatusId{ get; set; }

		[ XmlElement( ElementName = "StatusDefinition" ) ]
		public string DefinitionStr{ get; set; }

		[ XmlIgnore ]
		public ThreeDCartOrderStatusEnum Definition => 
			Enum.TryParse< ThreeDCartOrderStatusEnum >( this.DefinitionStr.Replace( " ", "" ), ignoreCase: true, out var result ) 
				? result : ThreeDCartOrderStatusEnum.Undefined;

		[ XmlElement( ElementName = "StatusText" ) ]
		public string Text{ get; set; }

		[ XmlElement( ElementName = "Visible" ) ]
		public bool Visible{ get; set; }
	}

	public enum ThreeDCartOrderStatusEnum
	{
		Undefined = 0,
		New = 1,
		Processing = 2,
		Partial = 3,
		Shipped = 4,
		Cancel = 5,
		Hold = 6,
		NotCompleted = 7,
		Custom1 = 8,
		Custom2 = 9,
		Custom3 = 10,
		Unpaid = 11,
		Review = 12
	}
}