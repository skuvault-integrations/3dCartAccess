using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using ThreeDCartAccess.Misc;

namespace ThreeDCartAccess.SoapApi.Misc
{
	public static class ErrorHelpers
	{
		/// <summary>Throw if the response indicates an error</summary>
		public static void ThrowIfError( XElement ordersResponse, string storeUrl, [ CallerMemberName ] string callerMethodName = "" )
		{
			var isResponseInvalid = ordersResponse.Name != null
						&& ordersResponse.Value != null
						&& ordersResponse.Name.LocalName == "Error"
						&& ordersResponse.Value.ToLower().Contains( "error" );

			if( isResponseInvalid )
			{
				var exception = new Exception( ordersResponse.Value );
				ThreeDCartLogger.Log.Trace( exception, "Error for {0}\tStoreUrl:{1}\tResponse:{2}", 
					callerMethodName, storeUrl, ordersResponse.Value );
				throw exception;
			}
		}
	}
}
