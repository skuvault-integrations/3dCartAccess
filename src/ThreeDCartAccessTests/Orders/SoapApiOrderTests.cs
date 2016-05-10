using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.SoapApi.Models.Configuration;

namespace ThreeDCartAccessTests.Orders
{
	public class SoapApiOrderTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			const string credentialsFilePath = @"..\..\Files\ThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< SoapApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey, testConfig.TimeZone );
			}
		}

		[ Test ]
		public void IsGetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.IsGetNewOrders( null, null );

			result.Should().Be( true );
		}

		[ Test ]
		public void GetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetNewOrders( new DateTime( 2014, 11, 26, 6, 54, 29 ), new DateTime( 2014, 11, 26, 6, 57, 15 ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetNewOrdersAsync( new DateTime( 2014, 11, 26, 6, 54, 29 ), new DateTime( 2014, 11, 26, 6, 57, 15 ) ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByNumber()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = service.GetOrdersByNumber( numbers, new DateTime( 2014, 11, 26, 6, 54, 29 ), new DateTime( 2014, 11, 26, 6, 57, 15 ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByNumberAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var numbers = new List< string > { "AB-1014" };
			var result = ( await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddHours( -4 ), DateTime.UtcNow ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrder()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetOrderByNumber( "AB-1014" );

			result.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetOrderAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetOrderByNumberAsync( "AB-1014" ) );

			result.Should().NotBeNull();
		}

		[ Test ]
		public void GetOrdersCount()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetOrdersCount();

			result.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersCountAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetOrdersCountAsync() );

			result.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrderStatuses()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = service.GetOrderStatuses().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrderStatusesAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapOrdersService( this.Config );
			var result = ( await service.GetOrderStatusesAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}
	}
}