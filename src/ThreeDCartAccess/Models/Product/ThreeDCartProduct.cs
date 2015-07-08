using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Product
{
	public class ThreeDCartProduct
	{
		[ XmlElement( ElementName = "ProductID" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "ProductName" ) ]
		public string ProductName{ get; set; }

		[ XmlElement( ElementName = "DateCreated" ) ]
		public string DateCreated{ get; set; }

		[ XmlElement( ElementName = "LastUpdate" ) ]
		public string LastUpdate{ get; set; }

		[ XmlElement( ElementName = "Mfgid" ) ]
		public string Mfgid{ get; set; }

		[ XmlElement( ElementName = "Manufacturer" ) ]
		public string Manufacturer{ get; set; }

		[ XmlElement( ElementName = "Distributor" ) ]
		public string Distributor{ get; set; }

		[ XmlElement( ElementName = "Cost" ) ]
		public decimal Cost{ get; set; }

		[ XmlElement( ElementName = "Price" ) ]
		public decimal Price{ get; set; }

		[ XmlElement( ElementName = "RetailPrice" ) ]
		public decimal RetailPrice{ get; set; }

		[ XmlElement( ElementName = "SalePrice" ) ]
		public decimal SalePrice{ get; set; }

		[ XmlIgnore ]
		public int Stock{ get; private set; }

		[ XmlElement( ElementName = "Stock" ) ]
		public decimal StockDecimal
		{
			get { return this._stockDecimal; }
			set
			{
				this._stockDecimal = value;
				this.Stock = ( int )value;
			}
		}

		private decimal _stockDecimal{ get; set; }

		[ XmlElement( ElementName = "StockAlert" ) ]
		public int StockAlert{ get; set; }

		[ XmlElement( ElementName = "Weight" ) ]
		public decimal Weight{ get; set; }

		[ XmlElement( ElementName = "Width" ) ]
		public decimal Width{ get; set; }

		[ XmlElement( ElementName = "Height" ) ]
		public decimal Height{ get; set; }

		[ XmlElement( ElementName = "Depth" ) ]
		public decimal Depth{ get; set; }

		[ XmlElement( ElementName = "MinimumOrder" ) ]
		public int MinimumOrder{ get; set; }

		[ XmlElement( ElementName = "MaximumOrder" ) ]
		public int MaximumOrder{ get; set; }

		[ XmlElement( ElementName = "Description" ) ]
		public string Description{ get; set; }

		[ XmlElement( ElementName = "ShipCost" ) ]
		public decimal ShipCost{ get; set; }

		[ XmlElement( ElementName = "Title" ) ]
		public string Title{ get; set; }

		[ XmlElement( ElementName = "OnSale" ) ]
		public int OnSale{ get; set; }

		[ XmlElement( ElementName = "HomeSpecial" ) ]
		public bool HomeSpecial{ get; set; }

		[ XmlElement( ElementName = "CategorySpecial" ) ]
		public bool CategorySpecial{ get; set; }

		[ XmlElement( ElementName = "NonSearchable" ) ]
		public bool NonSearchable{ get; set; }

		[ XmlElement( ElementName = "NonTax" ) ]
		public bool NonTax{ get; set; }

		[ XmlElement( ElementName = "NotForsale" ) ]
		public bool NotForsale{ get; set; }

		[ XmlElement( ElementName = "Hide" ) ]
		public bool Hide{ get; set; }

		[ XmlElement( ElementName = "GiftCertificate" ) ]
		public bool GiftCertificate{ get; set; }

		[ XmlElement( ElementName = "FreeShipping" ) ]
		public bool FreeShipping{ get; set; }

		[ XmlElement( ElementName = "SelfShip" ) ]
		public bool SelfShip{ get; set; }

		[ XmlElement( ElementName = "UserId" ) ]
		public string UserId{ get; set; }

		[ XmlElement( ElementName = "MinOrder" ) ]
		public string MinOrder{ get; set; }

		[ XmlElement( ElementName = "ListingDisplayType" ) ]
		public int ListingDisplayType{ get; set; }

		[ XmlElement( ElementName = "ShowOutStock" ) ]
		public int ShowOutStockInt{ get; set; }

		[ XmlIgnore ]
		public ShowOutStockEnum ShowOutStock
		{
			get { return ( ShowOutStockEnum )this.ShowOutStockInt; }
		}

		[ XmlElement( ElementName = "PricingGroupOpt" ) ]
		public bool PricingGroupOpt{ get; set; }

		[ XmlElement( ElementName = "QuantityDiscountOpt" ) ]
		public bool QuantityDiscountOpt{ get; set; }

		[ XmlElement( ElementName = "LoginLevel" ) ]
		public int LoginLevel{ get; set; }
	}

	public enum ShowOutStockEnum
	{
		Undefined = 0,
		Default = -1,
		OutOfStock = 1,
		BackOrder = 2,
		WaitingList = 3
	}
}