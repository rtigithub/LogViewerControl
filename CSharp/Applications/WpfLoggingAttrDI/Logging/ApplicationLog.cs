﻿using Microsoft.Extensions.Logging;

namespace WpfLoggingAttrDI;

public static partial class ApplicationLog
{
     #region Private Fields

     private const string AppName = "WpfLoggingAttrDI";

     #endregion Private Fields

     #region Public Methods

     [LoggerMessage(EventId = 0, EventName = AppName, Message = "{msg}")]
     public static partial void Emit(ILogger logger, LogLevel level, string msg);

     public static void Emit(ILogger logger, LogLevel level, string msg, Exception exception)
         => Emit(logger, level, $"{msg} - {exception}");

     #endregion Public Methods
}