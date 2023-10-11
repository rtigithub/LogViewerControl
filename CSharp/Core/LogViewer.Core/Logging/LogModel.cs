using Microsoft.Extensions.Logging;

namespace LogViewer.Core;

public class LogModel
{
     #region Public Properties

     public LogEntryColor? Color { get; set; }
     public EventId EventId { get; set; }
     public string? Exception { get; set; }
     public LogLevel LogLevel { get; set; }
     public object? State { get; set; }
     public DateTime Timestamp { get; set; }

     #endregion Public Properties
}