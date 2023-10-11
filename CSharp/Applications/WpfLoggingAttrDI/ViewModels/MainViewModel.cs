using LogViewer.Core.ViewModels;
using Mvvm.Core;

namespace WpfLoggingAttrDI.ViewModels;

public class MainViewModel : ViewModel
{
     #region Public Constructors

     public MainViewModel(LogViewerControlViewModel logViewer)
     {
          LogViewer = logViewer;
     }

     #endregion Public Constructors

     #region Public Properties

     public LogViewerControlViewModel LogViewer { get; }

     #endregion Public Properties
}