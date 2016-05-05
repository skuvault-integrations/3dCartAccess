using LINQtoCSV;

namespace ThreeDCartAccessTests
{
	internal class RestApiTestConfig
	{
		[ CsvColumn( Name = "StoreUrl", FieldIndex = 1 ) ]
		public string StoreUrl{ get; set; }

		[ CsvColumn( Name = "PrivateKey", FieldIndex = 2 ) ]
		public string PrivateKey{ get; set; }

		[ CsvColumn( Name = "Token", FieldIndex = 3 ) ]
		public string Token{ get; set; }
	}
}