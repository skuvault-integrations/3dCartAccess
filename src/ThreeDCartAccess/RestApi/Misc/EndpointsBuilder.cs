using System;

namespace ThreeDCartAccess.RestApi.Misc
{
	internal static class EndpointsBuilder
	{
		#region products	
		public static string GetProductsEnpoint( int offset, int limit, string catalogId = "" )
		{
			return string.Format( "/Products/{0}?offset={1}&limit={2}", catalogId, offset, limit );
		}
		#endregion

		#region orders	
		public static string GetOrdersEnpoint( int offset, int limit, string orderId = "" )
		{
			return string.Format( "/Orders/{0}?offset={1}&limit={2}", orderId, offset, limit );
		}

		public static string GetNewOrdersEnpoint( int offset, int limit, DateTime dateStartUtc, DateTime dateEndUtc, int timeZone, string orderId = "" )
		{
			return string.Format( "/Orders/{0}?offset={1}&limit={2}&datestart={3:MM/dd/yyyy}&dateend={4:MM/dd/yyyy}",
				orderId, offset, limit, dateStartUtc.AddHours( timeZone ), dateEndUtc.AddHours( timeZone ) );
		}
		#endregion
	}
}