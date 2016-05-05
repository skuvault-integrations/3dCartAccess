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
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.PrivateKey, testConfig.Token );
			}
		}

		[ Test ]
		public void GetAllOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetAllOrders();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetAllOrders2()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = new List< ThreeDCartOrder >();
			service.GetAllOrders( x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetAllOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = await service.GetAllOrdersAsync();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetAllOrdersAsync2()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = new List< ThreeDCartOrder >();
			await service.GetAllOrdersAsync( x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}
	}
}