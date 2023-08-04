using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ThreeDCartAccessTests.Integration.Orders
{
	public class SoapApiOrderTests : BaseSoapApiTests
	{
		[ SetUp ]
		public void Init()
		{
			base.GetCredentials();

			System.Net.ServicePointManager.SecurityProtocol |=  System.Net.SecurityProtocolType.Tls12;
			//Otherwise, get the error
			//	An error occurred while making the HTTP request to https://api.3dcart.com/cart.asmx. This could be due to the fact that the server certificate is not configured properly with HTTP.SYS in the HTTPS case. This could also be caused by a mismatch of the security binding between the client and the server.
		}

		[ Test ]
		[ Explicit ]
		public void IsGetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.IsGetNewOrders( null, null );

			result.Should().Be( true );
		}

		[ Test ]
		[ Explicit ]
		public void GetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetNewOrders( DateTime.UtcNow.AddDays( -4 ), DateTime.UtcNow ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetNewOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetNewOrdersAsync( DateTime.UtcNow.AddHours( -4 ), DateTime.UtcNow ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public void GetOrdersByNumber()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = service.GetOrdersByNumber( numbers, DateTime.UtcNow.AddHours( -4 ), DateTime.UtcNow ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetOrdersByNumberAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = ( await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddHours( -4 ), DateTime.UtcNow ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public void GetOrder()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetOrderByNumber( "AB-1014" );

			result.Should().NotBeNull();
		}

		[ Test ]
		[ Explicit ]
		public async Task GetOrderAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetOrderByNumberAsync( "AB-1014" ) );

			result.Should().NotBeNull();
		}

		[ Test ]
		[ Explicit ]
		public void GetOrdersCount()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetOrdersCount();

			result.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetOrdersCountAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetOrdersCountAsync() );

			result.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public void GetOrderStatuses()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetOrderStatuses().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetOrderStatusesAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetOrderStatusesAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}
	}
}