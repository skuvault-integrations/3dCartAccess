using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ThreeDCartAccess.RestApi;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.RestApi.Models.Order;

namespace ThreeDCartAccessTests.Orders
{
	public class RestApiOrderTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			const string credentialsFilePath = @"..\..\Files\RestApiThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< RestApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.PrivateKey, testConfig.Token, testConfig.TimeZone );
			}
		}

		[ Test ]
		public void GetAllOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetAllOrders();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetAllOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = await service.GetAllOrdersAsync();

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetNewOrders( new DateTime( 2014, 12, 2 ), new DateTime( 2014, 12, 4 ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetNewOrders2()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = new List< ThreeDCartOrder >();
			service.GetNewOrders( DateTime.UtcNow.AddDays( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = await service.GetNewOrdersAsync( DateTime.UtcNow.AddDays( -1 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrdersAsync2()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = new List< ThreeDCartOrder >();
			await service.GetNewOrdersAsync( DateTime.UtcNow.AddHours( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
	}
}