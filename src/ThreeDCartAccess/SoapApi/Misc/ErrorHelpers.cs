using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace ThreeDCartAccess.SoapApi.Misc
{
	public static class ErrorHelpers
	{
		/// <summary>Throw if the response indicates an error</summary>
		public static void ThrowIfError( XElement ordersResponse, string storeUrl, ILogger logger, [ CallerMemberName ] string callerMethodName = "" )
		{
			var isResponseInvalid = ordersResponse.Name != null
						&& ordersResponse.Value != null
						&& ordersResponse.Name.LocalName == "Error"
						&& ordersResponse.Value.ToLower().Contains( "error" );

			if( isResponseInvalid )
			{
				var exception = new Exception( ordersResponse.Value );
				logger.LogTrace( exception, "Error for {Error}\tStoreUrl:{Url}\tResponse:{Response}",
					callerMethodName, storeUrl, ordersResponse.Value );
				throw exception;
			}
		}
	}
}
