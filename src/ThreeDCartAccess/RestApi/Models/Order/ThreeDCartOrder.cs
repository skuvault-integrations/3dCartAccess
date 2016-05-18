using System;
using System.Collections.Generic;

namespace ThreeDCartAccess.RestApi.Models.Order
{
	public class ThreeDCartOrder
	{
		#region General Info
		public string InvoiceNumberPrefix{ get; set; }
		public int InvoiceNumber{ get; set; }

		public string FullInvoiceNumber
		{
			get { return this.InvoiceNumberPrefix + this.InvoiceNumber; }
		}

		public long? OrderID{ get; set; }
		public long? CustomerID{ get; set; }
		public DateTime? OrderDate{ get; set; }
		public DateTime OrderDateUtc{ get; private set; }
		public DateTime? LastUpdate{ get; set; }
		public DateTime LastUpdateUtc{ get; private set; }
		public string UserID{ get; set; }
		public string SalesPerson{ get; set; }
		public string ContinueURL{ get; set; }

		public ThreeDCartOrderStatusEnum OrderStatus{ get; private set; }

		public int OrderStatusID
		{
			get { return this._orderStatusID; }
			set
			{
				this._orderStatusID = value;
				this.OrderStatus = Enum.IsDefined( typeof( ThreeDCartOrderStatusEnum ), this.OrderStatusID ) ? ( ThreeDCartOrderStatusEnum )this.OrderStatusID : ThreeDCartOrderStatusEnum.Undefined;
			}
		}

		private int _orderStatusID = 0;

		public int TimeZone
		{
			get { return this._timeZone; }
			set
			{
				this._timeZone = value;

				if( this.OrderDate != null && this.OrderDate.Value != DateTime.MinValue )
					this.OrderDateUtc = this.OrderDate.Value.AddHours( -this._timeZone );

				if( this.LastUpdate != null && this.LastUpdate.Value != DateTime.MinValue )
					this.LastUpdateUtc = this.LastUpdate.Value.AddHours( -this._timeZone );
			}
		}

		private int _timeZone = -5;
		#endregion

		#region Billing Information
		public string BillingFirstName{ get; set; }
		public string BillingLastName{ get; set; }
		public string BillingCompany{ get; set; }
		public string BillingAddress{ get; set; }
		public string BillingAddress2{ get; set; }
		public string BillingCity{ get; set; }
		public string BillingState{ get; set; }
		public string BillingZipCode{ get; set; }
		public string BillingCountry{ get; set; }
		public string BillingPhoneNumber{ get; set; }
		public string BillingEmail{ get; set; }
		public string BillingPaymentMethod{ get; set; }
		public bool? BillingOnLinePayment{ get; set; }
		public string BillingPaymentMethodID{ get; set; }
		#endregion

		#region Shipping Information
		public List< ThreeDCartShipment > ShipmentList{ get; set; }
		#endregion

		#region Order Items
		public List< ThreeDCartOrderItem > OrderItemList{ get; set; }
		public decimal? OrderDiscount{ get; set; }
		public decimal? SalesTax{ get; set; }
		public decimal? SalesTax2{ get; set; }
		public decimal? SalesTax3{ get; set; }
		public decimal? OrderAmount{ get; set; }
		public decimal? AffiliateCommission{ get; set; }
		#endregion

		#region Transactions
		public List< ThreeDCartTransaction > TransactionList{ get; set; }
		#endregion

		#region Payment Information
		public string CardType{ get; set; }
		public string CardNumber{ get; set; }
		public string CardName{ get; set; }
		public string CardExpirationMonth{ get; set; }
		public string CardExpirationYear{ get; set; }
		public string CardIssueNumber{ get; set; }
		public string CardStartMonth{ get; set; }
		public string CardStartYear{ get; set; }
		public string CardAddress{ get; set; }
		public string CardVerification{ get; set; }
		#endregion

		#region Rewards
		public string RewardPoints{ get; set; }
		#endregion

		#region Checkout Questions
		public List< ThreeDCartQuestion > QuestionList{ get; set; }
		#endregion

		#region Referral Information
		public string Referer{ get; set; }
		public string IP{ get; set; }
		#endregion

		#region Order Comments
		public string CustomerComments{ get; set; }
		public string InternalComments{ get; set; }
		public string ExternalComments{ get; set; }
		#endregion
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