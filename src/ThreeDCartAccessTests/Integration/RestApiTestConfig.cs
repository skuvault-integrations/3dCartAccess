using LINQtoCSV;

namespace ThreeDCartAccessTests.Integration
{
	internal class RestApiTestConfig
	{
		[ CsvColumn( Name = "PrivateKey", FieldIndex = 1 ) ]
		public string PrivateKey{ get; set; }

		[ CsvColumn( Name = "StoreUrl", FieldIndex = 2 ) ]
		public string StoreUrl{ get; set; }

		[ CsvColumn( Name = "Token", FieldIndex = 3 ) ]
		public string Token{ get; set; }

		[ CsvColumn( Name = "TimeZone", FieldIndex = 4 ) ]
		public int TimeZone{ get; set; }
	}
}