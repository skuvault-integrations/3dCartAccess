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
		public List< Shipment > ShipmentList{ get; set; }
		#endregion

		#region Order Items
		public List< OrderItem > OrderItemList{ get; set; }
		public decimal? OrderDiscount{ get; set; }
		public decimal? SalesTax{ get; set; }
		public decimal? SalesTax2{ get; set; }
		public decimal? SalesTax3{ get; set; }
		public decimal? OrderAmount{ get; set; }
		public decimal? AffiliateCommission{ get; set; }
		#endregion

		#region Transactions
		public List< Transaction > TransactionList{ get; set; }
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
		public List< Question > QuestionList{ get; set; }
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

	#region Order Auxiliary Tables
	public class OrderItem
	{
		public int? CatalogID{ get; set; }
		public int? ItemIndexID{ get; set; }
		public string ItemID{ get; set; }
		public int? ItemShipmentID{ get; set; }
		public decimal? ItemQuantity{ get; set; }
		public int? ItemWarehouseID{ get; set; }
		public string ItemDescription{ get; set; }
		public decimal? ItemUnitPrice{ get; set; }
		public decimal? ItemWeight{ get; set; }
		public decimal? ItemOptionPrice{ get; set; }
		public string ItemAdditionalField1{ get; set; }
		public string ItemAdditionalField2{ get; set; }
		public string ItemAdditionalField3{ get; set; }
		public string ItemPageAdded{ get; set; }
		public DateTime? ItemDateAdded{ get; set; }
		public decimal? ItemUnitCost{ get; set; }
		public int? ItemUnitStock{ get; set; }
		public string ItemOptions{ get; set; }
		public string ItemCatalogIDOptions{ get; set; }
	}

	public class Transaction
	{
		public int? TransactionIndexID{ get; set; }
		public int? OrderID{ get; set; }
		public string TransactionID{ get; set; }
		public DateTime TransactionDateTime{ get; set; }
		public string TransactionType{ get; set; }
		public string TransactionMethod{ get; set; }
		public decimal? TransactionAmount{ get; set; }
		public string TransactionApproval{ get; set; }
		public string TransactionReference{ get; set; }
		public int? TransactionGatewayID{ get; set; }
		public string TransactionCVV2{ get; set; }
		public string TransactionAVS{ get; set; }
		public string TransactionResponseText{ get; set; }
		public string TransactionResponseCode{ get; set; }
		public int? TransactionCaptured{ get; set; }
	}

	public class Question
	{
		public int? OrderID{ get; set; }
		public int? QuestionID{ get; set; }
		public string QuestionTitle{ get; set; }
		public string QuestionAnswer{ get; set; }
		public string QuestionType{ get; set; }
		public int? QuestionCheckoutStep{ get; set; }
		public int? QuestionSorting{ get; set; }
		public int? QuestionDiscountGroup{ get; set; }
	}

	public class Shipment
	{
		public int? ShipmentID{ get; set; }
		public DateTime? ShipmentLastUpdate{ get; set; }
		public int? ShipmentBoxes{ get; set; }
		public string ShipmentInternalComment{ get; set; }
		public int? ShipmentOrderStatus{ get; set; }
		public string ShipmentAddress{ get; set; }
		public string ShipmentAddress2{ get; set; }
		public string ShipmentAlias{ get; set; }
		public string ShipmentCity{ get; set; }
		public string ShipmentCompany{ get; set; }
		public decimal? ShipmentCost{ get; set; }
		public string ShipmentCountry{ get; set; }
		public string ShipmentEmail{ get; set; }
		public string ShipmentFirstName{ get; set; }
		public string ShipmentLastName{ get; set; }
		public int? ShipmentMethodID{ get; set; }
		public string ShipmentMethodName{ get; set; }
		public string ShipmentShippedDate{ get; set; }
		public string ShipmentPhone{ get; set; }
		public string ShipmentState{ get; set; }
		public string ShipmentZipCode{ get; set; }
		public decimal? ShipmentTax{ get; set; }
		public decimal? ShipmentWeight{ get; set; }
		public string ShipmentTrackingCode{ get; set; }
		public string ShipmentUserID{ get; set; }
		public int? ShipmentNumber{ get; set; }
		public int? ShipmentAddressTypeID{ get; set; }
	}
	#endregion

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