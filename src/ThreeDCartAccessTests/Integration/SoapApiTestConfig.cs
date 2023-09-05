using LINQtoCSV;

namespace ThreeDCartAccessTests.Integration
{
	internal class SoapApiTestConfig
	{
		[ CsvColumn( Name = "StoreUrl", FieldIndex = 1 ) ]
		public string StoreUrl{ get; set; }

		[ CsvColumn( Name = "UserKey", FieldIndex = 2 ) ]
		public string UserKey{ get; set; }

		[ CsvColumn( Name = "TimeZone", FieldIndex = 3 ) ]
		public int TimeZone{ get; set; }
	}
}