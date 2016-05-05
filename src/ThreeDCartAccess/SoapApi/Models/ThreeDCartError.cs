using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.SoapApi.Models
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "Error" ) ]
	public class ThreeDCartError
	{
		[ XmlText ]
		public string Message{ get; set; }

		[ XmlElement ]
		public int Id{ get; set; }

		[ XmlElement ]
		public string Description{ get; set; }
	}
}