using Avalonia.Controls;
using MultiWindowApp.Services;
 
namespace MultiWindowApp.Views;
 
public partial class MainWindow : Window
{
    public MainWindow(WindowService service)
    {
        InitializeComponent();
        // Реєструємо себе у сервісі як батьківське вікно
        service.SetMainWindow(this);
    }
}