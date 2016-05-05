namespace ThreeDCartAccess.RestApi.Misc
{
	internal static class EndpointsBuilder
	{
		#region products	
		public static string GetAllProductsEnpoint( int offset, int limit, string catalogId = "" )
		{
			return string.Format( "/Products/{0}?offset={1}&limit={2}", catalogId, offset, limit );
		}
		#endregion
	}
}