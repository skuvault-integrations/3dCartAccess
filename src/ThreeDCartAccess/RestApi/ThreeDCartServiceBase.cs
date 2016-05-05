using System;
using CuttingEdge.Conditions;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccess.RestApi
{
	public abstract class ThreeDCartServiceBase
	{
		protected readonly ThreeDCartConfig Config;
		internal readonly WebRequestServices WebRequestServices;
		protected const int BatchSize = 600;

		protected ThreeDCartServiceBase( ThreeDCartConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this.Config = config;
			this.WebRequestServices = new WebRequestServices( config );
		}

		protected string GetMarker()
		{
			return Guid.NewGuid().ToString();
		}
	}
}