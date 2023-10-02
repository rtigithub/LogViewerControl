using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Mvvm.Core;

public class ObservableObject : ObservableRecipient
{
     protected new bool SetProperty<TValue>(ref TValue field, TValue newValue, [CallerMemberName] string? propertyName = null)
     {
          return base.SetProperty(ref field, newValue, propertyName);
     }
}
