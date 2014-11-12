﻿using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ThreeDCartAccess.Misc
{
	public static class XmlSerializeHelpers
	{
		public static string Serialize< T >( this T obj )
		{
			var serializer = new XmlSerializer( typeof( T ) );
			using( StringWriter writer = new Utf8StringWriter() )
			{
				serializer.Serialize( writer, obj );
				return writer.ToString();
			}
		}

		public static T Deserialize< T >( this string xml )
		{
			var serializer = new XmlSerializer( typeof( T ) );
			var result = ( T )serializer.Deserialize( new StringReader( xml ) );
			return result;
		}
	}

	public class Utf8StringWriter: StringWriter
	{
		public override Encoding Encoding
		{
			get { return Encoding.UTF8; }
		}
	}
}