using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartProduct
	{
		#region Pannel 1 - Product Information

		#region General Info
		public ThreeDCartProductSKU SKUInfo{ get; set; }
		public string MFGID{ get; set; }
		public string ShortDescription{ get; set; }
		public long? ManufacturerID{ get; set; }
		public List< ThreeDCartProductDistributor > DistributorList{ get; set; }
		public DateTime? LastUpdate{ get; set; }
		public string UserID{ get; set; }
		public string GTIN{ get; set; }
		public List< ThreeDCartProductCategory > CategoryList{ get; set; }
		#endregion

		#region Pricing
		#endregion

		#region Options
		public bool? NonTaxable{ get; set; }
		public bool? NotForSale{ get; set; }
		public bool? Hide{ get; set; }
		public bool? GiftCertificate{ get; set; }
		public bool? HomeSpecial{ get; set; }
		public bool? CategorySpecial{ get; set; }
		public bool? NonSearchable{ get; set; }
		public bool? GiftWrapItem{ get; set; }
		#endregion

		#region Shipping Options
		public double? ShipCost{ get; set; }
		public double? Weight{ get; set; }
		public double? Height{ get; set; }
		public double? Width{ get; set; }
		public double? Depth{ get; set; }
		public bool? SelfShip{ get; set; }
		public bool? FreeShipping{ get; set; }
		#endregion

		#region Reward Options
		public int? RewardPoints{ get; set; }
		public int? RedeemPoints{ get; set; }
		public bool? DisableRewards{ get; set; }
		#endregion

		#region Inventory Options
		public int InventoryControl{ get; set; }
		public int? StockAlert{ get; set; }
		public int? ReorderQuantity{ get; set; }
		public string InStockMessage{ get; set; }
		public string OutOfStockMessage{ get; set; }
		public string BackOrderMessage{ get; set; }

		[ IgnoreDataMember ]
		public ThreeDCartInventoryControlEnum InventoryControlEnum
		{
			get { return ( ThreeDCartInventoryControlEnum )this.InventoryControl; }
		}
		#endregion

		#region Warehouse Information
		public string WarehouseLocation{ get; set; }
		public string WarehouseBin{ get; set; }
		public string WarehouseAisle{ get; set; }
		public string WarehouseCustom{ get; set; }
		#endregion

		#region Description
		public string Description{ get; set; }
		#endregion

		#region Keywords
		public string Keywords{ get; set; }
		#endregion

		#region Custom Fields
		public string ExtraField1{ get; set; }
		public string ExtraField2{ get; set; }
		public string ExtraField3{ get; set; }
		public string ExtraField4{ get; set; }
		public string ExtraField5{ get; set; }
		public string ExtraField6{ get; set; }
		public string ExtraField7{ get; set; }
		public string ExtraField8{ get; set; }
		public string ExtraField9{ get; set; }
		public string ExtraField10{ get; set; }
		public string ExtraField11{ get; set; }
		public string ExtraField12{ get; set; }
		public string ExtraField13{ get; set; }
		#endregion

		#region Product Features
		public List< ThreeDCartFeature > FeatureList{ get; set; }
		#endregion

		#region Plugins

		//public SerializableDictionary<string, string> PluginList { get; set; }
		#endregion

		#region left out - to do
		//left out - pannel 1
		//categoriesaaa	nvarchar
		//display_stock	nvarchar
		//asm options
		//one box, multi boxes
		//product features - 
		#endregion

		#endregion

		#region Pannel 2 - Images
		public string MainImageFile{ get; set; }
		public string MainImageCaption{ get; set; }
		public string ThumbnailFile{ get; set; }
		public string MediaFile{ get; set; }
		public string AdditionalImageFile2{ get; set; }
		public string AdditionalImageCaption2{ get; set; }
		public string AdditionalImageFile3{ get; set; }
		public string AdditionalImageCaption3{ get; set; }
		public string AdditionalImageFile4{ get; set; }
		public string AdditionalImageCaption4{ get; set; }
		public List< ThreeDCartImageGallery > ImageGalleryList{ get; set; }
		#endregion

		#region Pannel 3 - Options
		public List< ThreeDCartOptionSet > OptionSetList{ get; set; }
		public List< ThreeDCartAdvancedOption > AdvancedOptionList{ get; set; }
		#endregion

		#region Pannel 4 - Related
		public List< ThreeDCartRelatedProduct > RelatedProductList{ get; set; }
		public List< ThreeDCartUpSellingItem > UpSellingItemList{ get; set; }
		#endregion

		#region Pannel 5 - Discount
		public List< ThreeDCartDiscount > DiscountList{ get; set; }
		#endregion

		#region Pannel 6 Advanced

		#region General Options
		public bool? DoNotUseCategoryOptions{ get; set; }
		public DateTime? DateCreated{ get; set; }
		public int? ListingTemplateID{ get; set; }
		public string ListingTemplateName{ get; set; }
		public int? LoginRequiredOptionID{ get; set; }
		public string LoginRequiredOptionName{ get; set; }
		public string LoginRequiredOptionRedirectTo{ get; set; }
		public int? AllowAccessCustomerGroupID{ get; set; }
		public string AllowAccessCustomerGroupName{ get; set; }
		public string RMAMaxPeriod{ get; set; }
		#endregion

		#region Price and Quantity Options
		//[ RegularExpression( @"^.{1,6}$" ) ] // valid 1 to 6 characters
		public string TaxCode{ get; set; }

		public string DisplayText{ get; set; }
		public double? MinimumQuantity{ get; set; }
		public double? MaximumQuantity{ get; set; }
		public bool? AllowOnlyMultiples{ get; set; }
		public bool? AllowFractionalQuantity{ get; set; }
		public string QuantityOptions{ get; set; }
		public bool? GroupOptionsForQuantityPricing{ get; set; }
		public bool? ApplyQuantityDiscountToOptions{ get; set; }
		#endregion

		#region Make an Offer
		public bool? EnableMakeAnOfferFeature{ get; set; }
		public string MinimumAcceptableOffer{ get; set; }
		#endregion

		#region Price Levels
		public double? PriceLevel1{ get; set; }
		public bool? PriceLevel1Hide{ get; set; }
		public double? PriceLevel2{ get; set; }
		public bool? PriceLevel2Hide{ get; set; }
		public double? PriceLevel3{ get; set; }
		public bool? PriceLevel3Hide{ get; set; }
		public double? PriceLevel4{ get; set; }
		public bool? PriceLevel4Hide{ get; set; }
		public double? PriceLevel5{ get; set; }
		public bool? PriceLevel5Hide{ get; set; }
		public double? PriceLevel6{ get; set; }
		public bool? PriceLevel6Hide{ get; set; }
		public double? PriceLevel7{ get; set; }
		public bool? PriceLevel7Hide{ get; set; }
		public double? PriceLevel8{ get; set; }
		public bool? PriceLevel8Hide{ get; set; }
		public double? PriceLevel9{ get; set; }
		public bool? PriceLevel9Hide{ get; set; }
		public double? PriceLevel10{ get; set; }
		public bool? PriceLevel10Hide{ get; set; }
		#endregion

		#region Buy Button
		public string BuyButtonLink{ get; set; }
		#endregion

		#region Link to this Product
		public string ProductLink{ get; set; }
		#endregion

		#endregion

		#region Pannel 7 - Meta Tags
		public string Title{ get; set; }
		public string CustomFileName{ get; set; }
		public string RedirectLink{ get; set; }
		public string MetaTags{ get; set; }
		#endregion

		#region Pannel 8 - eProducts
		public string SpecialInstructions{ get; set; }
		public bool? AssignKey{ get; set; }
		public bool? ReUseKeys{ get; set; }
		public List< ThreeDCartSerial > SerialList{ get; set; }
		public List< ThreeDCartEProduct > EProductList{ get; set; }
		#endregion
	}

	public enum ThreeDCartInventoryControlEnum
	{
		Undefined = 0,
		Default = -1,
		OutOfStock = 1,
		BackOrder = 2,
		WaitingList = 3
	}
}