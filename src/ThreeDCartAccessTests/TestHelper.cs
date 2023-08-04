using Microsoft.Extensions.Logging;

namespace ThreeDCartAccessTests
{
    public static class TestHelper
    {
        public static ILogger CreateLogger() => LoggerFactory.Create( builder => builder.AddConsole() ).CreateLogger( "Tests" );
    }
}