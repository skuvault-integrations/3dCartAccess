using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeDCartAccess.RestApi.Misc;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.RestApi.Models.Order;

namespace ThreeDCartAccess.RestApi
{
	public class ThreeDCartOrdersService: ThreeDCartServiceBase, IThreeDCartOrdersService
	{
		public ThreeDCartOrdersService( ThreeDCartConfig config ): base( config )
		{
		}

		#region Get All Orders
		public List< ThreeDCartOrder > GetAllOrders()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartOrder >();
			this.GetCollection< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetAllOrdersEnpoint( offset, BatchSize ), portion => result.AddRange( portion ) );
			return result;
		}

		public void GetAllOrders( Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			this.GetCollection< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetAllOrdersEnpoint( offset, BatchSize ), portion =>
			{
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}

		public async Task< List< ThreeDCartOrder > > GetAllOrdersAsync()
		{
			var marker = this.GetMarker();
			var result = new List< ThreeDCartOrder >();
			await this.GetCollectionAsync< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetAllOrdersEnpoint( offset, BatchSize ), portion => result.AddRange( portion ) );
			return result;
		}

		public async Task GetAllOrdersAsync( Action< ThreeDCartOrder > processAction )
		{
			var marker = this.GetMarker();
			await this.GetCollectionAsync< ThreeDCartOrder >( marker, offset => EndpointsBuilder.GetAllOrdersEnpoint( offset, BatchSize ), portion =>
			{
				foreach( var product in portion )
				{
					processAction( product );
				}
			} );
		}
		#endregion
	}
}