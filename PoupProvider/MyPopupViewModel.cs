using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoupProvider;

public partial class MyPopupViewModel : ObservableObject
{
    public TaskCompletionSource<List<string>> Task { get; set; }

    [RelayCommand]
    public void ResultSend()
    {
        Task.SetResult(new List<string> { "Result1", "Result2", "Result3" });
    }
}
