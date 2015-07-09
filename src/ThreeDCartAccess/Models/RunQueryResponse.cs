using System;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Models
{
	[ Serializable() ]
	[ XmlRoot( ElementName = "runQueryResponse" ) ]
	public class RunQueryResponse
	{
		[ XmlElement( ElementName = "Error" ) ]
		public ThreeDCartError Error{ get; set; }
	}
}