using Microsoft.Extensions.Logging;
using SkuVault.Integrations.Core.Logging;

namespace ThreeDCartAccess.Logging;

public class ThreeDCartLogger : IIntegrationLogger
{
	private readonly ILogger< ThreeDCartLogger > _logger;

	public ThreeDCartLogger( ILogger< ThreeDCartLogger > logger, LoggingContext loggingContext )
	{
		_logger = logger;
		LoggingContext = loggingContext;
	}

	public ILogger Logger => _logger;

	public LoggingContext LoggingContext{ get; }
}