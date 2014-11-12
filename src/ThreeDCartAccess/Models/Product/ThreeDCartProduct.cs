using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Product
{
	public class ThreeDCartProduct
	{
		[ XmlElement( ElementName = "ProductId" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "ProductName" ) ]
		public string ProductName{ get; set; }

		[ XmlElement( ElementName = "Mfgid" ) ]
		public string Mfgid{ get; set; }

		[ XmlElement( ElementName = "Manufacturer" ) ]
		public string Manufacturer{ get; set; }

		[ XmlElement( ElementName = "Distributor" ) ]
		public string Distributor{ get; set; }

		[ XmlElement( ElementName = "Cost" ) ]
		public string Cost{ get; set; }

		[ XmlElement( ElementName = "Price" ) ]
		public string Price{ get; set; }

		[ XmlElement( ElementName = "RetailPrice" ) ]
		public string RetailPrice{ get; set; }

		[ XmlElement( ElementName = "SalePrice" ) ]
		public string SalePrice{ get; set; }

		[ XmlElement( ElementName = "OnSale" ) ]
		public string OnSale{ get; set; }

		[ XmlElement( ElementName = "Stock" ) ]
		public string Stock{ get; set; }

		[ XmlElement( ElementName = "StockAlert" ) ]
		public string StockAlert{ get; set; }

		[ XmlElement( ElementName = "Weight" ) ]
		public string Weight{ get; set; }

		[ XmlElement( ElementName = "Width" ) ]
		public string Width{ get; set; }

		[ XmlElement( ElementName = "Height" ) ]
		public string Height{ get; set; }

		[ XmlElement( ElementName = "Depth" ) ]
		public string Depth{ get; set; }

		[ XmlElement( ElementName = "MinimumOrder" ) ]
		public string MinimumOrder{ get; set; }

		[ XmlElement( ElementName = "MaximumOrder" ) ]
		public string MaximumOrder{ get; set; }

		[ XmlElement( ElementName = "DateCreated" ) ]
		public string DateCreated{ get; set; }

		[ XmlElement( ElementName = "Description" ) ]
		public string Description{ get; set; }

		[ XmlElement( ElementName = "ShipCost" ) ]
		public string ShipCost{ get; set; }

		[ XmlElement( ElementName = "Title" ) ]
		public string Title{ get; set; }

		[ XmlElement( ElementName = "HomeSpecial" ) ]
		public string HomeSpecial{ get; set; }

		[ XmlElement( ElementName = "CategorySpecial" ) ]
		public string CategorySpecial{ get; set; }

		[ XmlElement( ElementName = "Hide" ) ]
		public string Hide{ get; set; }

		[ XmlElement( ElementName = "FreeShipping" ) ]
		public string FreeShipping{ get; set; }

		[ XmlElement( ElementName = "NonTax" ) ]
		public string NonTax{ get; set; }

		[ XmlElement( ElementName = "NotForsale" ) ]
		public string NotForsale{ get; set; }

		[ XmlElement( ElementName = "GiftCertificate" ) ]
		public string GiftCertificate{ get; set; }

		[ XmlElement( ElementName = "UserId" ) ]
		public string UserId{ get; set; }

		[ XmlElement( ElementName = "LastUpdate" ) ]
		public string LastUpdate{ get; set; }

		[ XmlElement( ElementName = "MinOrder" ) ]
		public string MinOrder{ get; set; }

		[ XmlElement( ElementName = "ListingDisplayType" ) ]
		public string ListingDisplayType{ get; set; }

		[ XmlElement( ElementName = "ShowOutStock" ) ]
		public string ShowOutStock{ get; set; }

		[ XmlElement( ElementName = "PricingGroupOpt" ) ]
		public string PricingGroupOpt{ get; set; }

		[ XmlElement( ElementName = "QuantityDiscountOpt" ) ]
		public string QuantityDiscountOpt{ get; set; }

		[ XmlElement( ElementName = "LoginLevel" ) ]
		public string LoginLevel{ get; set; }

		[ XmlElement( ElementName = "SelfShip" ) ]
		public string SelfShip{ get; set; }

		[ XmlElement( ElementName = "NonSearchable" ) ]
		public string NonSearchable{ get; set; }
	}
}