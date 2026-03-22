using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MultiWindowApp.Models;
 
namespace MultiWindowApp.ViewModels;
 
public partial class SecondViewModel : ObservableObject
{
    [ObservableProperty]
    private string _displayText;
 
    [ObservableProperty]
    private string _windowTitle = "Second window";
 
    public SecondViewModel(UserData? data)
    {
        if (data is null)
        {
            _displayText = "Data not received!\nOpen dialog first";
        }
        else
        {
            WindowTitle  = $"Data: {data.Name}";
            _displayText =
                $"Name:  {data.Name}\n" +
                $"E-mail: {data.Email}\n" +
                $"Age:   {data.Age}\n" +
                $"Admin: {(data.IsAdmin ? "Yes" : "No")}";
        }
    }
 
    [RelayCommand]
    private void Close() => CloseRequested?.Invoke(this, EventArgs.Empty);
 
    public event EventHandler? CloseRequested;
}