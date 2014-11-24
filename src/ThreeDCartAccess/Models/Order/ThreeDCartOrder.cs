using System.Xml.Serialization;
using ThreeDCartAccess.Misc;

namespace ThreeDCartAccess.Models.Order
{
	public class ThreeDCartOrder
	{
		[ XmlElement( ElementName = "OrderID" ) ]
		public long OrderId{ get; set; }

		[ XmlElement( ElementName = "InvoiceNumber" ) ]
		public string InvoiceNumber{ get; set; }

		[ XmlElement( ElementName = "CustomerID" ) ]
		public long CustomerId{ get; set; }

		[ XmlElement( ElementName = "DateStarted" ) ]
		public string DateStarted{ get; set; }

		[ XmlElement( ElementName = "LastUpdate" ) ]
		public string LastUpdate{ get; set; }

		[ XmlElement( ElementName = "Date" ) ]
		public string Date{ get; set; }

		[ XmlElement( ElementName = "Time" ) ]
		public string Time{ get; set; }

		[ XmlElement( ElementName = "Total" ) ]
		public decimal Total{ get; set; }

		[ XmlElement( ElementName = "Shipping" ) ]
		public decimal Shipping{ get; set; }

		[ XmlElement( ElementName = "PaymentMethod" ) ]
		public string PaymentMethod{ get; set; }

		[ XmlElement( ElementName = "Discount" ) ]
		public decimal Discount{ get; set; }

		[ XmlElement( ElementName = "OrderStatus" ) ]
		public string OrderStatusStr{ get; set; }

		[ XmlIgnore ]
		public ThreeDCartOrderStatusEnum OrderStatus
		{
			get { return this.OrderStatusStr.ToEnum< ThreeDCartOrderStatusEnum >(); }
		}

		[ XmlElement( ElementName = "Referer" ) ]
		public string Referer{ get; set; }

		[ XmlElement( ElementName = "SalesPerson" ) ]
		public string SalesPerson{ get; set; }

		[ XmlElement( ElementName = "UserID" ) ]
		public string UserId{ get; set; }

		[ XmlElement( ElementName = "Weight" ) ]
		public decimal Weight{ get; set; }

		[ XmlElement( ElementName = "BillingAddress" ) ]
		public ThreeDCartBillingAddress BillingAddress{ get; set; }

		[ XmlElement( ElementName = "ShippingInformation" ) ]
		public ThreeDCartShippingInformation ShippingInformation{ get; set; }
	}
}