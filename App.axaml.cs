using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MultiWindowApp.Services;
using MultiWindowApp.ViewModels;
using MultiWindowApp.Views;
 
namespace MultiWindowApp;
 
public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);
 
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Ручна ін'єкція залежностей
            var windowService = new WindowService();
            var mainVm        = new MainViewModel(windowService);
            var mainWindow    = new MainWindow(windowService)
            {
                DataContext = mainVm
            };
            desktop.MainWindow = mainWindow;
        }
        base.OnFrameworkInitializationCompleted();
    }
}