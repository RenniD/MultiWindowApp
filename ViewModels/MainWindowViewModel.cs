using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MultiWindowApp.Models;
using MultiWindowApp.Services;
 
namespace MultiWindowApp.ViewModels;
 
public partial class MainViewModel : ObservableObject
{
    private readonly WindowService _windowService;
 
    [ObservableProperty]
    private string _statusText = "No data to view";
 
    [ObservableProperty]
    private UserData? _lastUserData;
 
    public MainViewModel(WindowService windowService)
    {
        _windowService = windowService;
    }
 
    // Відкрити модальний діалог і отримати результат
    [RelayCommand]
    private async Task OpenDialog()
    {
        var dialogVm = new DialogViewModel();
        bool confirmed = await _windowService.ShowDialogAsync(dialogVm);
 
        if (confirmed)
        {
            LastUserData = dialogVm.GetResult();
            StatusText = $"Get: {LastUserData.Name}, {LastUserData.Email}";
        }
        else
        {
            StatusText = "Dialog closed";
        }
    }
 
    // Відкрити немодальне вікно
    [RelayCommand]
    private void OpenSecondWindow()
    {
        var vm = new SecondViewModel(LastUserData);
        _windowService.ShowWindow(vm);
    }
 
    // Пункт меню «Про програму»
    [RelayCommand]
    private async Task ShowAbout()
    {
        await _windowService.ShowMessageAsync(
            "About:",
            "MultiWindowApp v1.0\nAvalonia 11.3.8 | .NET 8");
    }
 
    // Пункт меню «Вихід»
    [RelayCommand]
    private void Exit()
    {
        System.Environment.Exit(0);
    }
}