using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace PoupProvider;

public partial class ViewModel : ObservableObject
{
    public ViewModel()
    {
        WeakReferenceMessenger.Default.Register<PopupMessage>(Global.obj, (obj, message) =>
        {
            PopupContent = message.Content;
        });

    }

    [ObservableProperty]
    private object _popupContent;


    [RelayCommand]
    public async Task Test()
    {
        var value = await new PopupProvider().GetPopupItems();
        Console.WriteLine(value);
    }
}

//create global singleton class
public static class Global
{
    public static void EmptyMethod() { }

    public static object obj = new object();
}

public class PopupMessage
{
    public object Content { get; set; }
}

public class PopupProvider
{
    public async Task<List<string>> GetPopupItems()
    {
        var popup = new MyPopup();
        var vm  = new MyPopupViewModel();
        popup.DataContext = vm;
        vm.Task = new TaskCompletionSource<List<string>>();
        WeakReferenceMessenger.Default.Send(new PopupMessage { Content = popup });
        var data = await vm.Task.Task;
        return data;
    }
}