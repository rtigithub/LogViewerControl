using System.Collections.Concurrent;
using LogViewer.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MsLogger.Core;

public class DataStoreLoggerProvider : ILoggerProvider
{
     #region Protected Fields

     protected readonly ILogDataStore _dataStore;

     protected readonly ConcurrentDictionary<string, DataStoreLogger> _loggers = new();

     #endregion Protected Fields

     #region Private Fields

     private readonly IDisposable? _onChangeToken;

     private DataStoreLoggerConfiguration _currentConfig;

     #endregion Private Fields

     #region Public Constructors

     public DataStoreLoggerProvider(IOptionsMonitor<DataStoreLoggerConfiguration> config, ILogDataStore dataStore)
     {
          _dataStore = dataStore;
          _currentConfig = config.CurrentValue;
          _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
     }

     #endregion Public Constructors

     #region Public Methods

     public ILogger CreateLogger(string categoryName)
        => _loggers.GetOrAdd(categoryName, name => new DataStoreLogger(name, GetCurrentConfig, _dataStore));

     public void Dispose()
     {
          _loggers.Clear();
          _onChangeToken?.Dispose();
     }

     #endregion Public Methods

     #region Protected Methods

     protected DataStoreLoggerConfiguration GetCurrentConfig()
             => _currentConfig;

     #endregion Protected Methods
}