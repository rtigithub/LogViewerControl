using Mvvm.Core;

namespace LogViewer.Core.ViewModels;

public class LogViewerControlViewModel : ViewModel, ILogDataStoreImpl
{
     #region Public Constructors

     public LogViewerControlViewModel(ILogDataStore dataStore)
     {
          DataStore = dataStore;
     }

     #endregion Public Constructors

     #region Public Properties

     public ILogDataStore DataStore { get; set; }

     #endregion Public Properties
}