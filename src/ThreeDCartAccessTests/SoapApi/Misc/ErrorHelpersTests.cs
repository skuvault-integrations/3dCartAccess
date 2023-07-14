using System;
using System.Xml;
using NUnit.Framework;
using ThreeDCartAccess.SoapApi.Misc;

namespace ThreeDCartAccessTests.SoapApi.Misc
{
	public class ErrorHelpersTests
	{
		[ Test ]
		public void ThrowIfError_ShouldThrowIfErrorInResponse()
		{
			var xmlDoc = new XmlDocument();
			var responseWithError = xmlDoc.CreateElement("Error");
			responseWithError.InnerText = "Error trying to get data from the store. Technical description: First request failed.<html>\n  <body>...";
			const string storeUrl = "www.some-store.abc";

			Assert.Throws< Exception >( () =>
				ErrorHelpers.ThrowIfError( responseWithError, storeUrl ) );
		}
	}
}