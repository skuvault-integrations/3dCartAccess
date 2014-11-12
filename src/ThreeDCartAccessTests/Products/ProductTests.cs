﻿using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.Models.Configuration;

namespace ThreeDCartAccessTests.Products
{
	public class ProductTests
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
		public void GetProducts()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = service.GetProducts();

			result.Should().NotBeNull();
			//result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetProductsAsync()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = await service.GetProductsAsync();

			result.Should().NotBeNull();
			//result.Count().Should().BeGreaterThan( 0 );
		}
	}
}