using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.Models.Configuration;

namespace ThreeDCartAccessTests.Orders
{
	public class OrderTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\ThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey );
			}
		}

		[ Test ]
		public void GetOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetOrders( null, null, true ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetOrdersAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByDate()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetOrders( DateTime.Now.AddDays( -1 ), DateTime.Now ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByDateAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetOrdersAsync( DateTime.Now.AddDays( -1 ), DateTime.Now ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersCount()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetOrdersCount();

			result.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersCountAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetOrdersCountAsync() );

			result.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrderStatuses()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetOrderStatuses().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrderStatusesAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetOrderStatusesAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}
	}
}