using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.V1;
using ThreeDCartAccess.V1.Models.Configuration;

namespace ThreeDCartAccessTests.Orders
{
	public class OrderTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			const string credentialsFilePath = @"..\..\Files\ThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey, testConfig.TimeZone );
			}
		}

		[ Test ]
		public void IsGetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.IsGetNewOrders( null, null );

			result.Should().Be( true );
		}

		[ Test ]
		public void GetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetNewOrders( null, null, true ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetNewOrdersAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetNewOrdersByDate()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetNewOrders( new DateTime( 2014, 11, 26, 6, 54, 29 ), new DateTime( 2014, 11, 26, 6, 57, 15 ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrdersByDateAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetNewOrdersAsync( DateTime.UtcNow.AddHours( -4 ), DateTime.UtcNow ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = service.GetOrders( numbers, null, null, true ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = ( await service.GetOrdersAsync( numbers ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByDate()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = service.GetOrders( numbers, new DateTime( 2014, 11, 26, 6, 54, 29 ), new DateTime( 2014, 11, 26, 6, 57, 15 ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByDateAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = ( await service.GetOrdersAsync( numbers, DateTime.UtcNow.AddHours( -4 ), DateTime.UtcNow ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrder()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetOrder( "AB-1014" );

			result.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetOrderAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetOrderAsync( "AB-1014" ) );

			result.Should().NotBeNull();
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