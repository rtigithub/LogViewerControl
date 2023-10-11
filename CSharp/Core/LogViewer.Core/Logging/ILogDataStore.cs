using System.Collections.ObjectModel;

namespace LogViewer.Core;

public interface ILogDataStore
{
     #region Public Properties

     ObservableCollection<LogModel> Entries { get; }

     #endregion Public Properties

     #region Public Methods

     void AddEntry(LogModel logModel);

     #endregion Public Methods
}