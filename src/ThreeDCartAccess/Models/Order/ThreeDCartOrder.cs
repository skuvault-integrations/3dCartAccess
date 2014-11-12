using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Order
{
	public class ThreeDCartOrder
	{
		[ XmlElement( ElementName = "OrderID" ) ]
		public string OrderID{ get; set; }

		[ XmlElement( ElementName = "InvoiceNumber" ) ]
		public string InvoiceNumber{ get; set; }

		[ XmlElement( ElementName = "CustomerID" ) ]
		public string CustomerID{ get; set; }

		[ XmlElement( ElementName = "Date" ) ]
		public string Date{ get; set; }

		[ XmlElement( ElementName = "Time" ) ]
		public string Time{ get; set; }

		[ XmlElement( ElementName = "Total" ) ]
		public string Total{ get; set; }

		[ XmlElement( ElementName = "Shipping" ) ]
		public string Shipping{ get; set; }

		[ XmlElement( ElementName = "PaymentMethod" ) ]
		public string PaymentMethod{ get; set; }

		[ XmlElement( ElementName = "Discount" ) ]
		public string Discount{ get; set; }

		[ XmlElement( ElementName = "OrderStatus" ) ]
		public string OrderStatus{ get; set; }

		[ XmlElement( ElementName = "Referer" ) ]
		public string Referer{ get; set; }

		[ XmlElement( ElementName = "DateStarted" ) ]
		public string DateStarted{ get; set; }

		[ XmlElement( ElementName = "UserID" ) ]
		public string UserID{ get; set; }

		[ XmlElement( ElementName = "LastUpdate" ) ]
		public string LastUpdate{ get; set; }

		[ XmlElement( ElementName = "Weight" ) ]
		public string Weight{ get; set; }

		[ XmlElement( ElementName = "BillingAddress" ) ]
		public ThreeDCartBillingAddress BillingAddress{ get; set; }

		[ XmlElement( ElementName = "ShippingInformation" ) ]
		public ThreeDCartShippingInformation ShippingInformation{ get; set; }
	}
}