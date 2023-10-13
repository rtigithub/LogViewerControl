﻿using LogViewer.Core.ViewModels;
using Mvvm.Core;

namespace WpfLoggingDI.ViewModels;

public class MainViewModel : ViewModel
{
     #region Constructor

     public MainViewModel(LogViewerControlViewModel logViewer)
     {
          LogViewer = logViewer;
     }

     #endregion Constructor

     #region Properties

     public LogViewerControlViewModel LogViewer { get; }

     #endregion Properties
}