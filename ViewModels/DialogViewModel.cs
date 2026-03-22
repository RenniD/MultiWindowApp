using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MultiWindowApp.Models;
 
namespace MultiWindowApp.ViewModels;
 
public partial class DialogViewModel : ObservableObject
{
    // [NotifyCanExecuteChangedFor] — при зміні Name автоматично
    // перевіряється CanExecute для ConfirmCommand (кнопка сіріє/активується)
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    private string _name = string.Empty;
 
    [ObservableProperty]
    private string _email = string.Empty;
 
    [ObservableProperty]
    private int _age = 18;
 
    [ObservableProperty]
    private bool _isAdmin;
 
    public bool DialogResult { get; private set; }
 
    // CanExecute — активна лише коли Name не порожній
    [RelayCommand(CanExecute = nameof(CanConfirm))]
    private void Confirm()
    {
        DialogResult = true;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }
    private bool CanConfirm() => !string.IsNullOrWhiteSpace(Name);
 
    [RelayCommand]
    private void Cancel()
    {
        DialogResult = false;
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }
 
    // Подія: ViewModel каже View «закрийся»
    public event EventHandler? CloseRequested;
 
    public UserData GetResult() => new()
    {
        Name    = Name,
        Email   = Email,
        Age     = Age,
        IsAdmin = IsAdmin
    };
}