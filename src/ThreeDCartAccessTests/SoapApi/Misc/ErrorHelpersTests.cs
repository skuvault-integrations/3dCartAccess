using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ThreeDCartAccess.SoapApi.Misc;

namespace ThreeDCartAccessTests.SoapApi.Misc
{
	public class ErrorHelpersTests
	{
		[ Test ]
		public void ThrowIfError_ShouldThrowIfErrorInResponse()
		{
			var responseWithError = new XElement( XName.Get( "Error" ) )
			{
				Value = "Error trying to get data from the store. Technical description: First request failed.<html>\n  <body>..."
			};
			const string storeUrl = "www.some-store.abc";

			Assert.Throws< Exception >(() => 
				ErrorHelpers.ThrowIfError( null, responseWithError, storeUrl ) );
		}
	}
}
