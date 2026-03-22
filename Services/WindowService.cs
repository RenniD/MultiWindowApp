using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using MultiWindowApp.ViewModels;
using MultiWindowApp.Views;
 
namespace MultiWindowApp.Services;
 
public class WindowService
{
    private Window? _mainWindow;
 
    public void SetMainWindow(Window window) => _mainWindow = window;
 
    // ═══ МОДАЛЬНИЙ ДІАЛОГ ═══
    // ShowDialog() — блокує батьківське вікно і чекає закриття
    public async Task<bool> ShowDialogAsync(DialogViewModel vm)
    {
        var dialog = new DialogWindow { DataContext = vm };
        var tcs = new TaskCompletionSource<bool>();
 
        vm.CloseRequested += (_, _) =>
        {
            tcs.TrySetResult(vm.DialogResult);
            dialog.Close();
        };
 
        await dialog.ShowDialog(_mainWindow!);
        return await tcs.Task;
    }
 
    // ═══ НЕМОДАЛЬНЕ ВІКНО ═══
    // Show() — обидва вікна активні одночасно
    public void ShowWindow(SecondViewModel vm)
    {
        var window = new SecondWindow { DataContext = vm };
        vm.CloseRequested += (_, _) => window.Close();
        window.Show(_mainWindow!);
    }
 
    // ═══ ПРОСТЕ ПОВІДОМЛЕННЯ ═══
    public async Task ShowMessageAsync(string title, string message)
    {
        var msgBox = new Window
        {
            Title   = title,
            Width   = 360,
            Height  = 160,
            Content = new Avalonia.Controls.TextBlock
            {
                Text    = message,
                Margin  = new Avalonia.Thickness(24),
                TextWrapping = Avalonia.Media.TextWrapping.Wrap
            }
        };
        await msgBox.ShowDialog(_mainWindow!);
    }
}