using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ThreeDCartAccess;
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
				this.ThreeDCartFactory = new ThreeDCartFactory( testConfig.PrivateKey );
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.Token, testConfig.TimeZone );
			}
		}

		[ Test ]
		public void IsGetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var result = service.IsGetNewOrders();

			result.Should().BeTrue();
		}

		#region Get New Orders
		[ Test ]
		public void GetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var result = service.GetNewOrders( new DateTime( 2014, 12, 2 ), new DateTime( 2014, 12, 4 ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetNewOrders2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var result = new List< ThreeDCartOrder >();
			service.GetNewOrders( DateTime.UtcNow.AddDays( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var result = await service.GetNewOrdersAsync( DateTime.UtcNow.AddDays( -1 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrdersAsync2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var result = new List< ThreeDCartOrder >();
			await service.GetNewOrdersAsync( DateTime.UtcNow.AddHours( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion

		#region Get Orders By Number
		[ Test ]
		public void GetOrdersByNumber()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var numbers = new List< string > { "AB-1043" };
			var result = service.GetOrdersByNumber( numbers, new DateTime( 2014, 12, 2 ), new DateTime( 2014, 12, 4 ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByNumber2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var numbers = new List< string > { "AB-1043", "AB-1042" };
			var result = new List< ThreeDCartOrder >();
			service.GetOrdersByNumber( numbers, DateTime.UtcNow.AddDays( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByNumberAsync()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var numbers = new List< string > { "AB-1043", "AB-1042" };
			var result = await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddDays( -100 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByNumberAsync2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.Config );
			var numbers = new List< string > { "AB-1043" };
			var result = new List< ThreeDCartOrder >();
			await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddHours( -9 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion
	}
}