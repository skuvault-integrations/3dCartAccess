using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Product
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "UpdateInventoryResponse" ) ]
	public class ThreeDCartUpdateInventory
	{
		[ XmlElement( ElementName = "ProductID" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "AO_Sufix" ) ]
		public string OptionCode{ get; set; }

		[ XmlElement( ElementName = "NewInventory" ) ]
		public int NewQuantity{ get; set; }

		/// <summary>
		/// Used only if <see cref="OptionCode"/> setted and <see cref="UpdateProductTotalStock"/> setted to true.
		/// </summary>
		public int OldQuantity{ get; set; }

		/// <summary>
		/// Update total product stock. Used only if <see cref="OptionCode"/> setted.
		/// </summary>
		public bool UpdateProductTotalStock{ get; set; }
	}
}