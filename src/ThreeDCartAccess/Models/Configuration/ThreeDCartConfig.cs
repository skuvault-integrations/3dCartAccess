﻿using CuttingEdge.Conditions;

namespace ThreeDCartAccess.Models.Configuration
{
	public sealed class ThreeDCartConfig
	{
		public string StoreUrl{ get; private set; }
		public string UserKey{ get; private set; }

		public ThreeDCartConfig( string storeUrl, string userKey )
		{
			Condition.Requires( storeUrl, "storeUrl" ).IsNotNullOrWhiteSpace();
			Condition.Requires( userKey, "userKey" ).IsNotNullOrWhiteSpace();

			storeUrl = storeUrl.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" ).Replace( "www.", "" );

			this.StoreUrl = storeUrl;
			this.UserKey = userKey;
		}
	}
}