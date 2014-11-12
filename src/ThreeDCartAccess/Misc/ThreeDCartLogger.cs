using Netco.Logging;

namespace ThreeDCartAccess.Misc
{
	public static class ThreeDCartLogger
	{
		public static ILogger Log{ get; private set; }

		static ThreeDCartLogger()
		{
			Log = NetcoLogger.GetLogger( "3dCartLogger" );
		}
	}
}