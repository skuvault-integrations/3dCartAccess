using System.Xml.Serialization;

namespace ThreeDCartAccess.Models.Product
{
	public class ThreeDCartInventory
	{
		[ XmlElement( ElementName = "id" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "name" ) ]
		public string Name{ get; set; }

		[ XmlElement( ElementName = "stock" ) ]
		public int Stock{ get; set; }

		[ XmlElement( ElementName = "AO_Code" ) ]
		public string OptionId{ get; set; }

		[ XmlIgnore ]
		public bool IsProductOption
		{
			get { return !string.IsNullOrEmpty( this.OptionId ); }
		}

		[ XmlElement( ElementName = "AO_Sufix" ) ]
		public string OptionCode{ get; set; }

		[ XmlElement( ElementName = "AO_Name" ) ]
		public string OptionName{ get; set; }

		[ XmlElement( ElementName = "AO_Stock" ) ]
		public string OptionStockStr
		{
			get { return this.OptionStock.ToString(); }
			set
			{
				if( !string.IsNullOrEmpty( value ) )
					this.OptionStock = int.Parse( value );
			}
		}

		[ XmlIgnore ]
		public int OptionStock{ get; set; }
	}
}