using System.Collections.ObjectModel;

namespace LogViewer.Core;

public class LogDataStore : ILogDataStore
{
     #region Private Fields

     private static readonly SemaphoreSlim _semaphore = new(initialCount: 1);

     #endregion Private Fields

     #region Public Properties

     public ObservableCollection<LogModel> Entries { get; } = new();

     #endregion Public Properties

     #region Public Methods

     public virtual void AddEntry(LogModel logModel)
     {
          // ensure only one operation at time from multiple threads
          _semaphore.Wait();

          Entries.Add(logModel);

          _semaphore.Release();
     }

     #endregion Public Methods
}