using System.Globalization;
using System.Xml.Serialization;

namespace ThreeDCartAccess.V1.Models.Product
{
	public class ThreeDCartInventory
	{
		[ XmlElement( ElementName = "id" ) ]
		public string ProductId{ get; set; }

		[ XmlElement( ElementName = "name" ) ]
		public string Name{ get; set; }

		[ XmlIgnore ]
		public int Stock{ get; private set; }

		[ XmlElement( ElementName = "stock" ) ]
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

		[ XmlIgnore ]
		public int OptionStock{ get; set; }

		[ XmlIgnore ]
		public decimal OptionStockDecimal{ get; set; }

		[ XmlElement( ElementName = "AO_Stock" ) ]
		public string OptionStockStr
		{
			get { return this.OptionStockDecimal.ToString(); }
			set
			{
				if( !string.IsNullOrEmpty( value ) )
				{
					this.OptionStockDecimal = decimal.Parse( value, CultureInfo.InvariantCulture );
					this.OptionStock = ( int )this.OptionStockDecimal;
				}
			}
		}

		[ XmlElement( ElementName = "show_out_stock" ) ]
		public int ShowOutStockInt{ get; set; }

		[ XmlIgnore ]
		public ShowOutStockEnum ShowOutStock
		{
			get { return ( ShowOutStockEnum )this.ShowOutStockInt; }
		}
	}
}