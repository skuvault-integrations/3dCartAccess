using System;
using System.Globalization;
using System.Xml.Serialization;
using Netco.Extensions;

namespace ThreeDCartAccess.SoapApi.Models.Order
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
		public string DateTimeStartedStr{ get; set; }

		[ XmlIgnore ]
		public DateTime DateTimeStartedUtc
		{
			get { return this.GetDateInUtc( this.DateTimeStartedStr, ref this._dateTimeStartedUtc ); }
		}

		private DateTime _dateTimeStartedUtc = DateTime.MinValue;

		[ XmlElement( ElementName = "LastUpdate" ) ]
		public string DateTimeUpdatedStr{ get; set; }

		[ XmlIgnore ]
		public DateTime DateTimeUpdatedUtc
		{
			get { return this.GetDateInUtc( this.DateTimeUpdatedStr, ref this._dateTimeUpdatedUtc ); }
		}

		private DateTime _dateTimeUpdatedUtc = DateTime.MinValue;

		[ XmlElement( ElementName = "Date" ) ]
		public string DateCreatedStr{ get; set; }

		[ XmlElement( ElementName = "Time" ) ]
		public string TimeCreatedStr{ get; set; }

		[ XmlIgnore ]
		public DateTime DateTimeCreatedUtc
		{
			get { return this.GetDateInUtc( this.DateCreatedStr, this.TimeCreatedStr, ref this._dateTimeCreatedUtc ); }
		}

		private DateTime _dateTimeCreatedUtc = DateTime.MinValue;

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
			get { return this.OrderStatusStr.ToEnum( ThreeDCartOrderStatusEnum.Undefined ); }
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

		#region misc
		private static readonly CultureInfo _culture = new CultureInfo( "en-US" );

		[ XmlIgnore ]
		public int TimeZone{ get; internal set; }

		private DateTime GetDateInUtc( string dateTime, ref DateTime cachedDateTime )
		{
			if( cachedDateTime != DateTime.MinValue )
				return cachedDateTime;

			var tmp = DateTime.Parse( dateTime, _culture );
			cachedDateTime = tmp.AddHours( -this.TimeZone );

			return cachedDateTime;
		}

		private DateTime GetDateInUtc( string date, string time, ref DateTime cachedDateTime )
		{
			if( cachedDateTime != DateTime.MinValue )
				return cachedDateTime;

			var dateCreated = DateTime.Parse( date, _culture );
			var timeCreated = DateTime.Parse( time, _culture );
			var tmp = dateCreated.Add( new TimeSpan( timeCreated.Hour, timeCreated.Minute, timeCreated.Second ) );
			cachedDateTime = tmp.AddHours( -this.TimeZone );

			return cachedDateTime;
		}
		#endregion
	}
}